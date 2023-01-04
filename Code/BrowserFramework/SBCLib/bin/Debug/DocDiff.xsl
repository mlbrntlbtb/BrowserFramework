<?xml version="1.0" encoding="ISO-8859-1"?>

<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
  <html>
  <body>
  <h2>DocDiff General Information</h2>
  <table border="1">
<xsl:for-each select="DocDiff/info">

      <tr> <th bgcolor="#0099FF"> Error Count</th>  <td> <font color="red"> <b> <xsl:value-of select="errorcount"/> </b></font> </td> </tr>
      <tr> <th bgcolor="#0099FF"> Config File</th> <td> <xsl:value-of select="configfile"/></td></tr>
      <tr> <th bgcolor="#0099FF"> Expected File</th>  <td><xsl:value-of select="expectedfile"/></td> </tr>
      <tr> <th bgcolor="#0099FF"> Actual File</th> <td><xsl:value-of select="actualfile"/></td> </tr>
      <tr> <th bgcolor="#0099FF"> Compare Type</th> <td><xsl:value-of select="comparetype"/></td> </tr>
      <tr> <th bgcolor="#0099FF"> Execution Time</th> <td><xsl:value-of select="executiontime"/></td> </tr>
</xsl:for-each>
   
  </table>

<h2>DocDiff Comparison Details</h2>
<table border="1">
    <tr bgcolor="#0099FF">
      
      <th>Test Type</th>
      <th>Test Details</th>
      <th>ExpectedFile Data</th>
      <th>ActualFile Data</th>
    </tr>
    <xsl:for-each select="DocDiff/results/result">
    <tr>
     
      <td><xsl:value-of select="testtype"/></td>
      <td><xsl:value-of select="testdetails"/></td>
      <td><xsl:value-of select="expectedfiledata"/></td>
      <td><xsl:value-of select="actualfiledata"/></td>
    </tr>
    </xsl:for-each>
  </table>

  </body>
  </html>
</xsl:template>

</xsl:stylesheet> 