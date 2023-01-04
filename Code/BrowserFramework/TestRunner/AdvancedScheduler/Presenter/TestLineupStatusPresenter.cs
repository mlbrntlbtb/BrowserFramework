using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.AdvancedScheduler.Model;
using TestRunner.AdvancedScheduler.View;

namespace TestRunner.AdvancedScheduler.Presenter
{
    public class TestLineupStatusPresenter
    {
		private readonly ITestLineupStatusView mView;

		public TestLineupStatusPresenter(ITestLineupStatusView view) => mView = view;

		/// <summary>
		/// Will determin what to log based on the status type on the status bar
		/// </summary>
		/// <param name="Record"></param>
		public void LogRecord(TestLineupRecord record)
		{
			var currentAgentName = record.AssignedAgentName == Agent.ANY_AGENT_NAME 
				? record.PrevAgentName 
				: record.AssignedAgentName;
			var agent = mView.AgentsPool.FirstOrDefault(a => a.Name == currentAgentName);

			switch (record.Status)
			{
				case Enumerations.TestStatus.Running:
					LogStatus("Suite is running.");
					break;
				case Enumerations.TestStatus.Passed:
					LogStatus("Suite passed.");
					break;
				case Enumerations.TestStatus.Failed:
					LogStatus("Suite failed.");
					break;
				case Enumerations.TestStatus.Cancelled:
					LogStatus("Cancelled by user.");
					break;
				case Enumerations.TestStatus.Error:
					LogStatus(GetStatusMessage("error"));
					break;
				case Enumerations.TestStatus.Warning:
					LogStatus(GetStatusMessage("warning"));
					break;
				case Enumerations.TestStatus.Disconnected:
					LogStatus("Controller and agent have been disconnected.");
					break;
				case Enumerations.TestStatus.Pending:
					if (record.AssignedAgentName == Agent.LOCAL_MACHINE &&
						mView.CheckIfLocalMachineBusy())
						LogStatus("Local machine is busy as of the moment.");
					else if (record.AssignedAgentName == Agent.LOCAL_MACHINE) break;
					else if (mView.AgentsPool.All(a =>
						 a.Status == Enumerations.AgentStatus.Busy
						)) LogStatus("All agents are busy as of the moment.");
					else if (mView.AgentsPool.All(a =>
						 a.Status == Enumerations.AgentStatus.Disabled
						)) LogStatus("All agents are disabled as of the moment. Enable any agent in the Agents tab to proceed.");

					else if (mView.AgentsPool.All(a =>
						 a.Status == Enumerations.AgentStatus.Error
						)) LogStatus("All agents returns error as of the moment. View more details in the Agents tab.");

					else if (mView.AgentsPool.All(a =>
						 a.Status == Enumerations.AgentStatus.Offline
						)) LogStatus("All agents are offline as of the moment. Launch the Scheduling Agent in any agent to proceed.");

					else if (mView.AgentsPool.All(a =>
						 a.Status == Enumerations.AgentStatus.Warning
						)) LogStatus("All agents returns warning as of the moment. View more details in the Agents tab.");

					else if (mView.AgentsPool.All(a => record.AssignedAgentName == Agent.ANY_AGENT_NAME && (
					   a.Status == Enumerations.AgentStatus.Busy ||
					   a.Status == Enumerations.AgentStatus.Disabled ||
					   a.Status == Enumerations.AgentStatus.Error ||
					   a.Status == Enumerations.AgentStatus.Offline ||
					   a.Status == Enumerations.AgentStatus.Warning
						))) LogStatus("All agents are not available as of the moment.");

					else if (mView.AgentsPool.All(a =>
						a.Status == Enumerations.AgentStatus.Busy ||
						a.Status == Enumerations.AgentStatus.Disabled ||
						a.Status == Enumerations.AgentStatus.Error ||
						a.Status == Enumerations.AgentStatus.Offline ||
						a.Status == Enumerations.AgentStatus.Warning
						))
					{
						string suggestion = agent.Status == Enumerations.AgentStatus.Error ? " View more details in the Agents tab." :
											agent.Status == Enumerations.AgentStatus.Warning ? " View more details in the Agents tab." :
											agent.Status == Enumerations.AgentStatus.Disabled ? " Enable the target agent in the Agents tab to proceed." :
											agent.Status == Enumerations.AgentStatus.Offline ? " Launch the Scheduling Agent in the target agent to proceed." :
											string.Empty;
						LogStatus($"The current status of agent {agent.Name} is {agent.Status}." + suggestion);
					}

					break;
			}

			void LogStatus(string message) => mView.TestLineupStatus.Log(record, agent, message);

			string GetStatusMessage(string customErrType)
			{
				try
				{
					var agentStat = AgentUtility.SendCommandToServer(currentAgentName, "status");
					var message = agentStat.Split('|').ElementAt(0).Split(':').ElementAt(1).Trim();
					return message;
				}
				catch (Exception e)
				{
					return $"Agent returns {customErrType}.";
				}
			}
		}
	}
}
