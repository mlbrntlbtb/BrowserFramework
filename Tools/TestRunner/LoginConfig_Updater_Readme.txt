[Created 6/13/2017] 

LoginConfig Updater v1.0 Guide

Contents:
A. Pre-requisites
B. Fields
C. How-To
   C-1. Update URL and/or Database of ALL environments in the LoginConfig
   C-2. Add/Edit an environment
D. FAQ
E. Other Questions

A. Pre-requisites:
	1. BrowserFramework is shared to the windows user using the tool (read and write access)
	
	2. VMConfig.xml has the following attributes in the vm tag:
		a. Name (MAKAPT577VS)
		b. Database (AutoDB_FWDev_04)
		c. Server (MAKAPT676VS)
		Note: The VMConfig.xml used is the controller’s (machine used to run the tool).

	3. The LoginConfig.xml of the VMs to be modified are checked out to avoid overwriting done by TestRunner upon running a suite due to getlatest.
	
	4. All VMs are recognized by their names:
		a. MAKAPT
		b. MAKC
		c. ASHAP
		d. MAKV

B. Fields:
	1. Product – specify which product’s VMConfig.exe would be used as reference 
	
	2. ID – the environment ID as specified in TestRunner (ie. ADMIN_MCMC)
	
	3. User – the user/username used to login to the application (ie. ADMIN)
	
	4. Password – the password used to login to the application (ie. 1234)
	
	5. URL – the web address of the application (ie. http://makapt618vs/DeltekPS10/app)
	
	6. Database – the database used to login to the application 
		a. Leave blank to use the database specified in the VMConfig.xml
		b. Specify a database (ie. AutoDB_FWDev_04 (MAKAPT676VS))
	
	7. Apply to – specify which VM’s would be affected by the update


C. How-To: 
	C-1. Update URL and/or Database of ALL environments in the LoginConfig
		Step 1: Leave ID blank
		Step 2: Tick the “Update” checkbox of the desired field/s to be affected by the change 
			Note: If you want to use the database specified in the VMConfig, check “Update” and leave it blank.
		Step 3: Fill in the fields 
		Step 4: Select VM/s to be affected by the update
		Step 5: Click Update and confirm changes 

		NOTE: Please be careful in using this feature since this would affect all the selected VMs and all the entries in their LoginConfig.xml.

	C-2. Add/Edit an environment
		Step 1: Fill in the ID 
			Note: If the environment ID is already existing, it would overwrite the existing ID with the specified fields. 
			Otherwise, it would create a new environment with the specified fields.
		Step 2: Fill in the desired fields
		Step 4: Select VM/s to be affected by the update
		Step 5: Click Update and confirm changes 
	 
D. Checking the logs
	You may check the changes made by opening LoginConfig_Updater.log. This is only available locally.
	Information included in log:
		i)	Date & time the updated was issued to the machines
		ii)	Windows User who performed update
		iii)	Type of change (Add/Edit)
		iv)	Virtual Machine affected
		v)	Environment ID
		vi)	User (Encrypted)
		vii)	Password (Encrypted)
		viii)	URL
		ix)	Database
		x)	Runtime errors (if any)

E. FAQ
	1. I keep getting the error message "Unable to open VMConfig.xml"
		- Ensure that the VMConfig.xml has the attributes specified above. The vm entry should look like this:
			<vm name="MAKAPT577VS" database="AutoDB_FWDev_04" server="MAKAPT676VS"/>
		You only need to update the controller's VMConfig to be able to run the tool properly.
	
	2. Log showing unable to update a specific VM only.
		- Check connection to VM by issuing a ping or access the shared folder of the VM using your machine.
	
	3. What will happen if I leave the "Apply to" field blank?
		- No VMs/machines will be affected
	
	4. Can I leave the User and/or Password fields blank?
		- Yes. Although you may see a value in the log, that is the encrypted value of a blank field.

F. Other Questions?
	Feel free to send me a message. 

- ShioriBermudo
