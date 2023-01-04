<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="xml" indent="yes"  omit-xml-declaration="yes"/>
  
    <xsl:strip-space elements="*" />
  
  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
  </xsl:template>
  
    <xsl:template match="objectstore">
      <xsl:variable name="screenName" select="@screen" />
      <xsl:variable name="lowercase" select="'abcdefghijklmnopqrstuvwxyz'" />
      <xsl:variable name="uppercase" select="'ABCDEFGHIJKLMNOPQRSTUVWXYZ'" />     
      
      <tests>
        <!-- Table Scripts -->
        <xsl:for-each select="control">
          <xsl:if test="controltype/text()='Table'">
            <xsl:apply-templates/>
          </xsl:if>
        </xsl:for-each>
        
        <!-- Verify Control Exists -->
        <test>
          <name>Verify<xsl:value-of select="translate($screenName, '_', '')"/>ControlExists</name>
          <file>C:\BrowserFramework\Products\DTM_B\Tests\Len\ClickIncompleteApplicationsTableFunction.xml</file>
          <identifier></identifier>
          <description>Auto Generated</description>
          <author>Auto Generated</author>
          <instance>-1</instance>
          <continueonerror>False</continueonerror>
          <status></status>
          <stepfailed>0</stepfailed>
          <start>1/1/0001 12:00:00 AM</start>
          <end>1/1/0001 12:00:00 AM</end>
          <elapsed></elapsed>
          <testsetup>
            <logmessages />
          </testsetup>
          <testteardown>
            <logmessages />
          </testteardown>
          <links />
          <tags />
          <steps>
          <xsl:for-each select="control">
            <xsl:variable name="controlName" select="@key" />
            <step>
              <xsl:attribute name="id"><xsl:value-of select = "position()" /></xsl:attribute>
              <execute>True</execute>
              <screen><xsl:value-of select="$screenName"/></screen>
              <control><xsl:value-of select="$controlName"/></control>
              <keyword>VerifyExists</keyword>
              <parameters>
                <parameter>D{<xsl:value-of select="translate($controlName,$uppercase,$lowercase)"/>}</parameter>
              </parameters>
              <delay>0</delay>
              <status>Not run</status>
              <logmessages />
              <start>1/1/0001 12:00:00 AM</start>
              <end>1/1/0001 12:00:00 AM</end>
              <elapsed></elapsed>
            </step>
          </xsl:for-each>
          </steps>
        </test>
        <xsl:text>&#xd;</xsl:text>
        <data>
          <xsl:attribute name="test">Verify<xsl:value-of select="translate($screenName, '_', '')"/>ControlExists</xsl:attribute>
          <datarecord name="qa_owner">
            <datavalue>Auto Generated</datavalue>
          </datarecord>
          <datarecord name="date_added">
            <datavalue>Auto Generated</datavalue>
          </datarecord>
          <datarecord name="notes">
            <datavalue>Verifying all controls exists</datavalue>
          </datarecord>

          <xsl:for-each select="control">
            <xsl:variable name="controlName" select="@key" />
            <datarecord>
              <xsl:attribute name="name"><xsl:value-of select="translate($controlName,$uppercase,$lowercase)"/></xsl:attribute>
              <datavalue>True</datavalue>
            </datarecord>
          </xsl:for-each>
        </data>
        
        <!-- Set Fields -->
        <xsl:if test="//controltype/text()='TextBox' or //controltype/text()='ComboBox' or //controltype/text()='RadioButton' or //controltype/text()='CheckBox' or //controltype/text()='MultiSelect'">
          <xsl:text>&#xd;</xsl:text>
          <test>
            <name>Set<xsl:value-of select="translate($screenName, '_', '')"/>Fields</name>
            <file>C:\BrowserFramework\Products\DTM_B\Tests\Len\ClickIncompleteApplicationsTableFunction.xml</file>
            <identifier></identifier>
            <description>Auto Generated</description>
            <author>Auto Generated</author>
            <instance>-1</instance>
            <continueonerror>False</continueonerror>
            <status></status>
            <stepfailed>0</stepfailed>
            <start>1/1/0001 12:00:00 AM</start>
            <end>1/1/0001 12:00:00 AM</end>
            <elapsed></elapsed>
            <testsetup>
              <logmessages />
            </testsetup>
            <testteardown>
              <logmessages />
            </testteardown>
            <links />
            <tags />
            <steps>
              <xsl:for-each select="control">
                <xsl:if test="controltype/text()='TextBox' or controltype/text()='ComboBox' or controltype/text()='RadioButton' or controltype/text()='CheckBox' or controltype/text()='MultiSelect'">
                  <xsl:apply-templates />
                </xsl:if>
              </xsl:for-each>
            </steps>
          </test>
          <xsl:text>&#xd;</xsl:text>
          <data>
            <xsl:attribute name="test">Set<xsl:value-of select="translate($screenName, '_', '')"/>Fields</xsl:attribute>
            <datarecord name="qa_owner">
              <datavalue>Auto Generated</datavalue>
            </datarecord>
            <datarecord name="date_added">
              <datavalue>Auto Generated</datavalue>
            </datarecord>
            <datarecord name="notes">
              <datavalue>Verifying all controls exists</datavalue>
            </datarecord>
            <xsl:for-each select="control">
              <xsl:if test="controltype/text()='TextBox' or controltype/text()='ComboBox' or controltype/text()='RadioButton' or controltype/text()='CheckBox' or controltype/text()='MultiSelect'">
                <xsl:variable name="controlName" select="@key" />
                <datarecord>
                  <xsl:attribute name="name">
                    <xsl:value-of select="translate($controlName,$uppercase,$lowercase)"/>
                  </xsl:attribute>
                  <datavalue>Auto Generated</datavalue>
                </datarecord>
              </xsl:if>
            </xsl:for-each>
          </data>
        </xsl:if>
        
        <!-- Verify Field Value -->
        <xsl:if test="//controltype/text()='Label' or //controltype/text()='TextBox' or //controltype/text()='MultiSelect' or //controltype/text()='ComboBox' or //controltype/text()='CheckBox' or //controltype/text()='RadioButton' or //controltype/text()='List'">
          <xsl:text>&#xd;</xsl:text>
          <test>
            <name>Verify<xsl:value-of select="translate($screenName, '_', '')"/>FieldValue</name>
            <file>C:\BrowserFramework\Products\DTM_B\Tests\Len\VerifyAddEditCustomerFields.xml</file>
            <identifier></identifier>
            <description>Auto Generated</description>
            <author>Auto Generated</author>
            <instance>-1</instance>
            <continueonerror>False</continueonerror>
            <status></status>
            <stepfailed>0</stepfailed>
            <start>1/1/0001 12:00:00 AM</start>
            <end>1/1/0001 12:00:00 AM</end>
            <elapsed></elapsed>
            <testsetup>
              <logmessages />
            </testsetup>
            <testteardown>
              <logmessages />
            </testteardown>
            <links />
            <tags />
            <steps>
              <step id="1">
                <execute>True</execute>
                <screen>Browser</screen>
                <control></control>
                <keyword>ExecuteKeyword</keyword>
                <parameters>
                  <parameter><xsl:value-of select="$screenName"/>q7*;D{control}q7*;D{keyword}q7*;D{parameter}</parameter>
                </parameters>
                <delay>0</delay>
                <status>Not run</status>
                <logmessages />
                <start>1/1/0001 12:00:00 AM</start>
                <end>1/1/0001 12:00:00 AM</end>
                <elapsed></elapsed>
              </step>
            </steps>
          </test>
          <xsl:text>&#xd;</xsl:text>
          <data>
            <xsl:attribute name="test">Verify<xsl:value-of select="translate($screenName, '_', '')"/>FieldValue</xsl:attribute>
            <datarecord name="qa_owner">
              <xsl:for-each select="control">
                <xsl:if test="controltype/text()='Label' or controltype/text()='TextBox' or controltype/text()='MultiSelect' or controltype/text()='ComboBox' or controltype/text()='CheckBox' or controltype/text()='RadioButton' or controltype/text()='List'">
                  <datavalue>Auto Generated</datavalue>
                </xsl:if>
              </xsl:for-each>
            </datarecord>
            <datarecord name="date_added">
              <xsl:for-each select="control">
                <xsl:if test="controltype/text()='Label' or controltype/text()='TextBox' or controltype/text()='MultiSelect' or controltype/text()='ComboBox' or controltype/text()='CheckBox' or controltype/text()='RadioButton' or controltype/text()='List'">
                  <datavalue>Auto Generated</datavalue>
                </xsl:if>
              </xsl:for-each>
            </datarecord>
            <datarecord name="notes">
              <xsl:for-each select="control">
                <xsl:if test="controltype/text()='Label' or controltype/text()='TextBox' or controltype/text()='MultiSelect' or controltype/text()='ComboBox' or controltype/text()='CheckBox' or controltype/text()='RadioButton' or controltype/text()='List'">
                  <datavalue>Auto Generated</datavalue>
                </xsl:if>
              </xsl:for-each>
            </datarecord>
            <datarecord name="control">
              <xsl:for-each select="control">
                <xsl:if test="controltype/text()='Label' or controltype/text()='TextBox' or controltype/text()='MultiSelect' or controltype/text()='ComboBox' or controltype/text()='CheckBox' or controltype/text()='RadioButton' or controltype/text()='List'">
                  <xsl:variable name="controlName" select="@key" />
                  <datavalue><xsl:value-of select="$controlName"/></datavalue>
                </xsl:if>
              </xsl:for-each>
            </datarecord>
            <datarecord name="keyword">
              <xsl:for-each select="control">
                <xsl:if test="controltype/text()='Label' or controltype/text()='TextBox' or controltype/text()='MultiSelect' or controltype/text()='ComboBox' or controltype/text()='CheckBox' or controltype/text()='RadioButton' or controltype/text()='List'">
                  <datavalue>
                    <xsl:choose>
                      <xsl:when test="controltype/text()='MultiSelect'">VerifyItemSelected</xsl:when>
                      <xsl:when test="controltype/text()='CheckBox'">VerifyValue</xsl:when>
                      <xsl:when test="controltype/text()='List'">VerifyItemExists</xsl:when>
                      <xsl:when test="controltype/text()='RadioButton'">VerifyValue</xsl:when>
                      <xsl:otherwise>VerifyText</xsl:otherwise>
                    </xsl:choose>
                  </datavalue>
                </xsl:if>
              </xsl:for-each>
            </datarecord>
            <datarecord name="parameter">
              <xsl:for-each select="control">
                <xsl:if test="controltype/text()='Label' or controltype/text()='TextBox' or controltype/text()='MultiSelect' or controltype/text()='ComboBox' or controltype/text()='CheckBox' or controltype/text()='RadioButton' or controltype/text()='List'">
                  <datavalue>Auto Generated, Please input correct parameters</datavalue>
                </xsl:if>
              </xsl:for-each>
            </datarecord>
          </data>
        </xsl:if>
        
        <!-- Click Control-->
        <xsl:if test="//controltype/text()='Link' or //controltype/text()='Button' or //controltype/text()='Image'">
          <xsl:text>&#xd;</xsl:text>
          <test>
            <name>Click<xsl:value-of select="translate($screenName, '_', '')"/>Control</name>
            <file>C:\BrowserFramework\Products\DTM_B\Tests\Len\VerifyAddEditCustomerFields.xml</file>
            <identifier></identifier>
            <description>Auto Generated</description>
            <author>Auto Generated</author>
            <instance>-1</instance>
            <continueonerror>False</continueonerror>
            <status></status>
            <stepfailed>0</stepfailed>
            <start>1/1/0001 12:00:00 AM</start>
            <end>1/1/0001 12:00:00 AM</end>
            <elapsed></elapsed>
            <testsetup>
              <logmessages />
            </testsetup>
            <testteardown>
              <logmessages />
            </testteardown>
            <links />
            <tags />
            <steps>
              <step id="1">
                <execute>True</execute>
                <screen>Browser</screen>
                <control></control>
                <keyword>ExecuteKeyword</keyword>
                <parameters>
                  <parameter><xsl:value-of select="$screenName"/>q7*;D{control}q7*;D{keyword}q7*;D{parameter}</parameter>
                </parameters>
                <delay>0</delay>
                <status>Not run</status>
                <logmessages />
                <start>1/1/0001 12:00:00 AM</start>
                <end>1/1/0001 12:00:00 AM</end>
                <elapsed></elapsed>
              </step>
            </steps>
          </test>
          <xsl:text>&#xd;</xsl:text>
          <data>
            <xsl:attribute name="test">Click<xsl:value-of select="translate($screenName, '_', '')"/>Control</xsl:attribute>
            <datarecord name="qa_owner">
              <xsl:for-each select="control">
                <xsl:if test="controltype/text()='Link' or controltype/text()='Button' or controltype/text()='Image'">
                  <datavalue>Auto Generated</datavalue>
                </xsl:if>
              </xsl:for-each>
            </datarecord>
            <datarecord name="date_added">
              <xsl:for-each select="control">
                <xsl:if test="controltype/text()='Link' or controltype/text()='Button' or controltype/text()='Image'">
                  <datavalue>Auto Generated</datavalue>
                </xsl:if>
              </xsl:for-each>
            </datarecord>
            <datarecord name="notes">
              <xsl:for-each select="control">
                <xsl:if test="controltype/text()='Link' or controltype/text()='Button' or controltype/text()='Image'">
                  <datavalue>Auto Generated</datavalue>
                </xsl:if>
              </xsl:for-each>
            </datarecord>
            <datarecord name="control">
              <xsl:for-each select="control">
                <xsl:if test="controltype/text()='Link' or controltype/text()='Button' or controltype/text()='Image'">
                  <xsl:variable name="controlName" select="@key" />
                  <datavalue>
                    <xsl:value-of select="$controlName"/>
                  </datavalue>
                </xsl:if>
              </xsl:for-each>
            </datarecord>
            <datarecord name="keyword">
              <xsl:for-each select="control">
                <xsl:if test="controltype/text()='Link' or controltype/text()='Button' or controltype/text()='Image'">
                  <datavalue>Click</datavalue>
                </xsl:if>
              </xsl:for-each>
            </datarecord>
            <datarecord name="parameter">
              <xsl:for-each select="control">
                <xsl:if test="controltype/text()='Link' or controltype/text()='Button' or controltype/text()='Image'">
                  <datavalue>
                    <xsl:value-of select="substring-before(' ',' ')"/>
                  </datavalue>
                </xsl:if>
              </xsl:for-each>
            </datarecord>
          </data>
        </xsl:if>

        <!-- Click Control-->
        <xsl:for-each select="control/controltype">
          <xsl:if test="./text()='Tab'">
            <xsl:variable name="controlName" select="../@key" />
            <xsl:text>&#xd;</xsl:text>
            <test>
              <name>Select<xsl:value-of select="translate($controlName, '_', '')"/></name>
              <file></file>
              <identifier></identifier>
              <description>Auto Generated</description>
              <author>Auto Generated</author>
              <instance>-1</instance>
              <continueonerror>False</continueonerror>
              <status></status>
              <stepfailed>0</stepfailed>
              <start>1/1/0001 12:00:00 AM</start>
              <end>1/1/0001 12:00:00 AM</end>
              <elapsed></elapsed>
              <testsetup>
                <logmessages />
              </testsetup>
              <testteardown>
                <logmessages />
              </testteardown>
              <links />
              <tags />
              <steps>
                <step id="1">
                  <execute>True</execute>
                  <screen>Browser</screen>
                  <control></control>
                  <keyword>ExecuteKeyword</keyword>
                  <parameters>
                    <parameter><xsl:value-of select="$screenName"/>q7*;<xsl:value-of select="$controlName"/>q7*;Selectq7*;D{caption}</parameter>
                  </parameters>
                  <delay>0</delay>
                  <status>Not run</status>
                  <logmessages />
                  <start>1/1/0001 12:00:00 AM</start>
                  <end>1/1/0001 12:00:00 AM</end>
                  <elapsed></elapsed>
                </step>
              </steps>
            </test>
            <xsl:text>&#xd;</xsl:text>
            <data>
              <xsl:attribute name="test">Select<xsl:value-of select="translate($screenName, '_', '')"/>Tab</xsl:attribute>
              <datarecord name="qa_owner"/>
              <datarecord name="date_added"/>
              <datarecord name="note"/>
              <datarecord name="caption"/>
            </data>
          </xsl:if>
        </xsl:for-each>
        <xsl:text>&#xd;</xsl:text>
      </tests>
    </xsl:template>
  
  <xsl:template match="controltype|searchmethod|searchparameters" />
  
  <xsl:template match="controltype[text()='Table']">
    <xsl:text>&#xd;</xsl:text>
    <test>
          <name>Click<xsl:value-of select="translate(../@key, '_', '')"/>Function</name>
          <file></file>
          <identifier></identifier>
          <description>Auto Generated</description>
          <author>Auto Generated</author>
          <instance>-1</instance>
          <continueonerror>False</continueonerror>
          <status></status>
          <stepfailed>0</stepfailed>
          <start>1/1/0001 12:00:00 AM</start>
          <end>1/1/0001 12:00:00 AM</end>
          <elapsed></elapsed>
          <testsetup>
            <logmessages />
          </testsetup>
          <testteardown>
            <logmessages />
          </testteardown>
          <links />
          <tags />
      <steps>
        <step id="1">
          <execute>True</execute>
          <screen>Function</screen>
          <control>Function</control>
          <keyword>IfThenElse</keyword>
          <parameters>
            <parameter>D{get_data}q7*;=q7*;Trueq7*;2q7*;3</parameter>
          </parameters>
          <delay>0</delay>
          <status>Not run</status>
          <logmessages />
          <start>1/1/0001 12:00:00 AM</start>
          <end>1/1/0001 12:00:00 AM</end>
          <elapsed></elapsed>
        </step>
        <step id="2">
          <execute>True</execute>
          <screen><xsl:value-of select ="../../@screen"/></screen>
          <control><xsl:value-of select="../@key"/></control>
          <keyword>GetTableRowWithColumnValue</keyword>
          <parameters>
            <parameter>1q7*;D{parameter_get}q7*;row</parameter>
          </parameters>
          <delay>0</delay>
          <status>Not run</status>
          <logmessages />
          <start>1/1/0001 12:00:00 AM</start>
          <end>1/1/0001 12:00:00 AM</end>
          <elapsed></elapsed>
        </step>
        <step id="3">
          <execute>True</execute>
          <screen>Browser</screen>
          <control></control>
          <keyword>ExecuteKeyword</keyword>
          <parameters>
            <parameter><xsl:value-of select ="../../@screen"/>q7*;<xsl:value-of select ="../@key"/>q7*;D{keyword_click}q7*;D{parameter_click}</parameter>
          </parameters>
          <delay>0</delay>
          <status>Not run</status>
          <logmessages />
          <start>1/1/0001 12:00:00 AM</start>
          <end>1/1/0001 12:00:00 AM</end>
          <elapsed></elapsed>
        </step>
      </steps>
        </test>
    <xsl:text>&#xd;</xsl:text>
    <data>
      <xsl:attribute name="test">Click<xsl:value-of select="translate(../@key, '_', '')"/>Function</xsl:attribute>
      <datarecord name="qa_owner"/>
      <datarecord name="date_added"/>
      <datarecord name="note"/>
      <datarecord name="get_data"/>
      <datarecord name="parameter_get"/>
      <datarecord name="keyword_click"/>
      <datarecord name="parameter_click"/>
    </data>
    <xsl:text>&#xd;</xsl:text>
    <test>
          <name>Set<xsl:value-of select="translate(../@key, '_', '')"/>Function</name>
          <file></file>
          <identifier></identifier>
          <description>Auto Generated</description>
          <author>Auto Generated</author>
          <instance>-1</instance>
          <continueonerror>False</continueonerror>
          <status></status>
          <stepfailed>0</stepfailed>
          <start>1/1/0001 12:00:00 AM</start>
          <end>1/1/0001 12:00:00 AM</end>
          <elapsed></elapsed>
          <testsetup>
            <logmessages />
          </testsetup>
          <testteardown>
            <logmessages />
          </testteardown>
          <links />
          <tags />
      <steps>
        <step id="1">
          <execute>True</execute>
          <screen>Function</screen>
          <control>Function</control>
          <keyword>IfThenElse</keyword>
          <parameters>
            <parameter>D{get_data}q7*;=q7*;Trueq7*;2q7*;3</parameter>
          </parameters>
          <delay>0</delay>
          <status>Not run</status>
          <logmessages />
          <start>1/1/0001 12:00:00 AM</start>
          <end>1/1/0001 12:00:00 AM</end>
          <elapsed></elapsed>
        </step>
        <step id="2">
          <execute>True</execute>
          <screen><xsl:value-of select ="../../@screen"/></screen>
          <control><xsl:value-of select="../@key"/></control>
          <keyword>GetTableRowWithColumnValue</keyword>
          <parameters>
            <parameter>1q7*;D{parameter_get}q7*;row</parameter>
          </parameters>
          <delay>0</delay>
          <status>Not run</status>
          <logmessages />
          <start>1/1/0001 12:00:00 AM</start>
          <end>1/1/0001 12:00:00 AM</end>
          <elapsed></elapsed>
        </step>
        <step id="3">
          <execute>True</execute>
          <screen>Browser</screen>
          <control></control>
          <keyword>ExecuteKeyword</keyword>
          <parameters>
            <parameter><xsl:value-of select ="../../@screen"/>q7*;<xsl:value-of select ="../@key"/>q7*;D{keyword_set}q7*;D{parameter_set}</parameter>
          </parameters>
          <delay>0</delay>
          <status>Not run</status>
          <logmessages />
          <start>1/1/0001 12:00:00 AM</start>
          <end>1/1/0001 12:00:00 AM</end>
          <elapsed></elapsed>
        </step>
      </steps>
        </test>
    <xsl:text>&#xd;</xsl:text>
    <data>
      <xsl:attribute name="test">Set<xsl:value-of select="translate(../@key, '_', '')"/>Function</xsl:attribute>
      <datarecord name="qa_owner"/>
      <datarecord name="date_added"/>
      <datarecord name="note"/>
      <datarecord name="get_data"/>
      <datarecord name="parameter_get"/>
      <datarecord name="keyword_set"/>
      <datarecord name="parameter_set"/>
    </data>
    <xsl:text>&#xd;</xsl:text>
    <test>
          <name>Verify<xsl:value-of select="translate(../@key, '_', '')"/>Data</name>
          <file></file>
          <identifier></identifier>
          <description>Auto Generated</description>
          <author>Auto Generated</author>
          <instance>-1</instance>
          <continueonerror>False</continueonerror>
          <status></status>
          <stepfailed>0</stepfailed>
          <start>1/1/0001 12:00:00 AM</start>
          <end>1/1/0001 12:00:00 AM</end>
          <elapsed></elapsed>
          <testsetup>
            <logmessages />
          </testsetup>
          <testteardown>
            <logmessages />
          </testteardown>
          <links />
          <tags />
      <steps>
        <step id="1">
          <execute>True</execute>
          <screen>Function</screen>
          <control>Function</control>
          <keyword>IfThenElse</keyword>
          <parameters>
            <parameter>D{get_data}q7*;=q7*;Trueq7*;2q7*;3</parameter>
          </parameters>
          <delay>0</delay>
          <status>Not run</status>
          <logmessages />
          <start>1/1/0001 12:00:00 AM</start>
          <end>1/1/0001 12:00:00 AM</end>
          <elapsed></elapsed>
        </step>
        <step id="2">
          <execute>True</execute>
          <screen><xsl:value-of select ="../../@screen"/></screen>
          <control><xsl:value-of select="../@key"/></control>
          <keyword>GetTableRowWithColumnValue</keyword>
          <parameters>
            <parameter>1q7*;D{parameter_get}q7*;row</parameter>
          </parameters>
          <delay>0</delay>
          <status>Not run</status>
          <logmessages />
          <start>1/1/0001 12:00:00 AM</start>
          <end>1/1/0001 12:00:00 AM</end>
          <elapsed></elapsed>
        </step>
        <step id="3">
          <execute>True</execute>
          <screen>Browser</screen>
          <control></control>
          <keyword>ExecuteKeyword</keyword>
          <parameters>
            <parameter><xsl:value-of select ="../../@screen"/>q7*;<xsl:value-of select ="../@key"/>q7*;D{keyword_verify}q7*;D{parameter_verify}</parameter>
          </parameters>
          <delay>0</delay>
          <status>Not run</status>
          <logmessages />
          <start>1/1/0001 12:00:00 AM</start>
          <end>1/1/0001 12:00:00 AM</end>
          <elapsed></elapsed>
        </step>
      </steps>
        </test>
    <xsl:text>&#xd;</xsl:text>
    <data>
      <xsl:attribute name="test">Verify<xsl:value-of select="translate(../@key, '_', '')"/>Data</xsl:attribute>
      <datarecord name="qa_owner"/>
      <datarecord name="date_added"/>
      <datarecord name="note"/>
      <datarecord name="get_data"/>
      <datarecord name="parameter_get"/>
      <datarecord name="keyword_verify"/>
      <datarecord name="parameter_verify"/>
    </data>
    <xsl:text>&#xd;</xsl:text>
  </xsl:template>

  <xsl:template match="controltype[text()='TextBox']|controltype[text()='RadioButton']|controltype[text()='CheckBox']|controltype[text()='MultiSelect']|controltype[text()='ComboBox']">
    <xsl:variable name="screenName" select="ancestor::objectstore/@screen" />
    <xsl:variable name="lowercase" select="'abcdefghijklmnopqrstuvwxyz'" />
    <xsl:variable name="uppercase" select="'ABCDEFGHIJKLMNOPQRSTUVWXYZ'" />    
    <xsl:variable name="controlName" select="../@key" />
    <step>
      <xsl:attribute name="id"><xsl:value-of select = "position()" /></xsl:attribute>
      <execute>True</execute>
      <screen><xsl:value-of select="$screenName"/></screen>
      <control><xsl:value-of select="$controlName"/></control>
      <keyword>
        <xsl:choose>
        <xsl:when test="text()='TextBox'">Set</xsl:when>
        <xsl:when test="text()='CheckBox'">Set</xsl:when>
        <xsl:otherwise>Select</xsl:otherwise>
        </xsl:choose>
      </keyword>
      <parameters>
        <parameter>D{<xsl:value-of select="translate($controlName,$uppercase,$lowercase)"/>}</parameter>
      </parameters>
      <delay>0</delay>
      <status>Not run</status>
      <logmessages />
      <start>1/1/0001 12:00:00 AM</start>
      <end>1/1/0001 12:00:00 AM</end>
      <elapsed></elapsed>
    </step>
  </xsl:template>

</xsl:stylesheet>
