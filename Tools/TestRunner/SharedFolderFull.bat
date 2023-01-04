SET p=%1
net share /d BrowserFramework /y
net share BrowserFramework=%p% /UNLIMITED /GRANT:EVERYONE,FULL
exit