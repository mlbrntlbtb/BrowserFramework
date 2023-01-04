using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CommonLib.DlkSystem;
using System.Security.Permissions;

namespace CommonLib.DlkRecords
{
    /// <summary>
    /// Tags class to use for Suites/Tests
    /// </summary>
    public class DlkTag
    {
        public const string STR_XML_TAGS = "tags";
        public const string STR_XML_TAG = "tag";
        public const string STR_XML_TAG_ID = "id";
        public const string STR_XML_TAG_NAME = "name";
        public const string STR_XML_TAG_DESCRIPTION = "description";
        public const string STR_XML_TAG_CONFLICTS = "conflicts";

        private const char CHAR_DEFAULT_CONFLICT_DELIMITER = ';';
        private string mConflictsString = string.Empty;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="TagName">Name</param>
        /// <param name="TagConflicts">Conflicts ID delimited by ';'</param>
        /// <param name="TagDescription">Description</param>
        public DlkTag(string TagName,  string TagConflicts, string TagDescription = "")
        {
            Id = Guid.NewGuid().ToString();
            Name = TagName;
            mConflictsString = TagConflicts;
            Description = TagDescription;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="TagId">Tag ID</param>
        /// <param name="TagName">Name</param>
        /// <param name="TagConflicts">Conflicts ID delimited by ';'</param>
        /// <param name="TagDescription">Description</param>
        public DlkTag(string TagId, string TagName, string TagConflicts, string TagDescription = "")
        {
            Id = TagId;
            Name = TagName;
            mConflictsString = TagConflicts;
            Description = TagDescription;
        }

        /// <summary>
        /// Tag ID in GUID format
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Tag name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tag description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Readonly property to return Conflicts IDs as string, delimited by ';' 
        /// </summary>
        public string Conflicts 
        { 
            get
            {
                return mConflictsString;
            } 
        }

        /// <summary>
        /// Readonly property to return Conflicts IDs as string array
        /// </summary>
        public string[] ConflictsIDList
        {
            get
            {
                return Conflicts.Split(CHAR_DEFAULT_CONFLICT_DELIMITER);
            }
        }

        /// <summary>
        /// Readonly property to return Tag Conflicts
        /// </summary>
        public DlkTag[] TagConflicts
        {
            get
            {
                return LoadAllTags().FindAll(x => ConflictsIDList.Contains(x.Id)).OrderBy(y => y.Name).ToArray();
            }
        }

        /// <summary>
        /// Add a tag conflict
        /// </summary>
        /// <param name="ConflictID">Tag Conflict ID</param>
        /// <returns>TRUE if Tag Conflict was added; FALSE if tag conflict already exists and no longer added</returns>
        public bool AddTagConflict(string ConflictID)
        {
            bool ret = false;
            if (!ConflictsIDList.Contains(ConflictID))
            {
                mConflictsString = mConflictsString.Trim(CHAR_DEFAULT_CONFLICT_DELIMITER); /* ensure no leading/trailing delimiter */
                mConflictsString += (CHAR_DEFAULT_CONFLICT_DELIMITER + ConflictID);
                mConflictsString = mConflictsString.Trim(CHAR_DEFAULT_CONFLICT_DELIMITER); /* ensure no leading/trailing delimiter */
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// Add multiple tag conflicts
        /// </summary>
        /// <param name="ConflictIDs">Array of Tag conflict IDs to add</param>
        /// <returns>Count of tag conflicts added</returns>
        public int AddTagConflicts(string[] ConflictIDs)
        {
            int ret = 0;
            foreach (string id in ConflictIDs)
            {
                if (AddTagConflict(id))
                {
                    ret++;
                }
            }
            return ret;
        }

        /// <summary>
        /// Replace tag conflicts with input list, OR clear tag conflicts if no input
        /// </summary>
        /// <param name="ConflictIDs">Tag conflicts IDs array to replace current tag conflicts</param>
        /// <returns>Count of updated tag conflicts</returns>
        public int ReplaceTagConflicts(string[] ConflictIDs = null)
        {
            mConflictsString = ConflictIDs == null ? string.Empty : string.Join(CHAR_DEFAULT_CONFLICT_DELIMITER.ToString(), ConflictIDs);
            return ConflictsIDList.Count();
        }

        /// <summary>
        /// Remove tag conflict
        /// </summary>
        /// <param name="ConflictID">ID of tag conflict to remove</param>
        /// <returns>TRUE if Tag Conflict was removed; FALSE if tag conflict not found</returns>
        public bool RemoveTagConflict(string ConflictID)
        {
            bool ret = false;
            if (ConflictsIDList.Contains(ConflictID))
            {
                mConflictsString = mConflictsString.Replace(ConflictID, string.Empty); /* remove */
                mConflictsString = mConflictsString.Replace(CHAR_DEFAULT_CONFLICT_DELIMITER.ToString() 
                    + CHAR_DEFAULT_CONFLICT_DELIMITER.ToString()
                    , CHAR_DEFAULT_CONFLICT_DELIMITER.ToString()); /* Ensure we don;t have ;; -> we removed from middle */
                mConflictsString = mConflictsString.Trim(CHAR_DEFAULT_CONFLICT_DELIMITER); /* ensure no leading/trailing delimiter -> removed from beginning/end */
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// Load all tags from central Tag file
        /// </summary>
        /// <returns>All tags from Tags file</returns>
        public static List<DlkTag> LoadAllTags()
        {
            /* Ensure tags file exists */
            if (!File.Exists(DlkEnvironment.mTagsFilePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(DlkEnvironment.mTagsFilePath));
                File.WriteAllText(DlkEnvironment.mTagsFilePath, "<tags />");
            }

            XDocument doc = XDocument.Load(DlkEnvironment.mTagsFilePath);

            var tags = from tag in doc.Descendants(STR_XML_TAG)
                       select new
                       {
                           id = tag.Attribute(STR_XML_TAG_ID).Value.ToString().Trim(),
                           name = tag.Attribute(STR_XML_TAG_NAME).Value.ToString().Trim(),
                           conflicts = tag.Attributes(STR_XML_TAG_CONFLICTS).Any() ? tag.Attribute(STR_XML_TAG_CONFLICTS).Value.ToString().Trim() : String.Empty,
                           description = tag.Attribute(STR_XML_TAG_DESCRIPTION).Value.ToString().Trim()
                       };

            /* Get Rows */
            List<DlkTag> lstTags = new List<DlkTag>();

            foreach (var itm in tags)
            {
                DlkTag tg = new DlkTag(itm.id, itm.name, itm.conflicts, itm.description);
                lstTags.Add(tg);
            }
            return lstTags;
        }

        /// <summary>
        /// Save tags to central Tag file
        /// </summary>
        public static void SaveTags(List<DlkTag> TagsToSave)
        {
            List<XElement> lstTagsNode = new List<XElement>();

            foreach (DlkTag tag in TagsToSave)
            {
                XElement tagnode = new XElement(STR_XML_TAG,
                    new XAttribute(STR_XML_TAG_ID, tag.Id),
                    new XAttribute(STR_XML_TAG_NAME, tag.Name),
                    new XAttribute(STR_XML_TAG_CONFLICTS, tag.Conflicts),
                    new XAttribute(STR_XML_TAG_DESCRIPTION, tag.Description)
                    );
                lstTagsNode.Add(tagnode);
            }

            XElement mRoot = new XElement(STR_XML_TAGS, lstTagsNode);

            File.SetAttributes(DlkEnvironment.mTagsFilePath, FileAttributes.Normal);
            FileIOPermission fPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, DlkEnvironment.mTagsFilePath);

            mRoot.Save(DlkEnvironment.mTagsFilePath);
        }
    }
}
