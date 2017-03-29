<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html"/>
  <xsl:template match="/">
    <style type="text/css">
      body { font-family: Calibri, Verdana, Arial, sans-serif; background-color: White; color: Black; }
      .header2,.header3,.header5 { margin: 0; padding: 0; }
      .header2 { border-top: solid 1px #f0f5fa; padding-top: 0.5em; }
      .header3 { font-weight: normal; }
      .header5 { font-weight: normal; font-style: italic; margin-bottom: 0.75em; }
	  .softlabel{ color:silver; margin-left:10px; margin-right:5px }
      pre { font-family: Consolas; font-size: 85%; margin: 0 0 0 1em; padding: 0; }
      .row, .altrow { padding: 0.1em 0.3em; }
      .row { background-color: #f0f5fa; }
      .altrow { background-color: #e1ebf4; }
      .success, .failure, .skipped { font-family: Arial Unicode MS; font-weight: normal; float: left; width: 1em; display: block; }
      .success { color: #0c0; }
      .failure { color: #c00; }
      .skipped { color: #cc0; }
      .timing { float: right; }
      .indent { margin: 0.25em 0 0.5em 2em; }
      .clickable { cursor: pointer; }
      .testcount { font-size: 85%; }
	  .failedTest {border-bottom:#F1F1F1; border-bottom-width:1px; border-bottom-style:solid; height:20px}
    </style>
    <script language="javascript">
      function ToggleClass(id) {
        var elem = document.getElementById(id);
        if (elem.style.display == "none")
          elem.style.display = "block";
        else
          elem.style.display = "none";
      }
    
	</script>
	
    <tr><td class="sectionheader" colspan="2">GXUnit 3.1 Test Results</td></tr>
    <tr>
      <td colspan="2">
        <div><b><u>Summary</u></b></div>
        <div>
          Suites run: <a href="#all"><b><xsl:value-of select="count(//GXUnitSuite/Suites/Suite/SuiteName)" /></b></a> &#160;
		  Tests run: <a href="#all"><b><xsl:value-of select="count(//GXUnitSuite/Suites/Suite/TestCases/TestCase/TestResult)" /></b></a> &#160;
          Failures: <a href="#failures"><b><xsl:value-of select="count(//GXUnitSuite/Suites/Suite/TestCases/TestCase/TestResult[normalize-space(text()) = 'FAIL'])" /></b></a>,
          Run time: <b><xsl:value-of select="round(sum(//GXUnitSuite/Suites/Suite/TestCases/TestCase/TestTimeExecution)*0.001)"/>s</b>
        </div>
        <br />
        
		<!-- ===================== FAILED TESTS ======================= -->
		<div><a name="failures"></a><b><u>Failed tests</u></b></div>
        <div class="header5">Click test class name to expand/collapse test details</div> 
		<div>
		<table id="FailedTests">
		<xsl:for-each select="//GXUnitSuite/Suites/Suite/TestCases/TestCase">
			<xsl:if test="TestResult='FAIL' or TestResuult='EXCEPTION'">
				<tr>
				<!-- Failed Test Header -->
				<td class="failedTest" width="15px"><span class="failure">&#x2718;</span></td>
				<td class="failedTest">
					<span class="clickable">
					        <xsl:attribute name="onclick">ToggleClass('Results_<xsl:value-of select="TestName"/>')</xsl:attribute>
							<xsl:attribute name="ondblclick">ToggleClass('Results_<xsl:value-of select="TestName"/>')</xsl:attribute>
						<xsl:value-of select="TestName"/>
					</span>
				</td>
				</tr><tr>
				<td colspan="2">
					<table cellspacing="0px" cellpadding="0px">
						<xsl:attribute name="id">Results_<xsl:value-of select="TestName"/></xsl:attribute>
						<xsl:for-each select=".//Asserts/Assert">
							<tr>
							<td width="15px"></td>
							<td width="15px"></td>
							<td><xsl:value-of select="Result"/></td>
							<td><span class="softlabel">Variable:</span><xsl:value-of select="Variable"/></td>
							<td><span class="softlabel">Expected:</span><xsl:value-of select="Expected"/></td>
							<td><span class="softlabel">Obtained:</span><xsl:value-of select="Obtained"/></td>
							</tr>
						</xsl:for-each>
					</table>
				</td>
				</tr>
				
			</xsl:if>
		</xsl:for-each>
		</table>
		</div>
		<br />

		<!-- ===================== ALL TESTS ======================= -->
		<div><a name="all"></a><b><u>All tests</u></b></div>
        <div class="header5">Click test class name to expand/collapse test details</div>
		<div><table id="allTests">
			<xsl:for-each select="//GXUnitSuite/Suites/Suite">
			<tr>
			<td width="15px">
				<xsl:if test="count(.//TestCases/TestCase/TestResult[normalize-space(text()) = 'FAIL']) + count(.//TestCases/TestCase/TestResult[normalize-space(text()) = 'EXCEPTION'])>0"><span class="failure">&#x2718;</span></xsl:if>
				<xsl:if test="count(.//TestCases/TestCase/TestResult[normalize-space(text()) = 'FAIL']) + count(.//TestCases/TestCase/TestResult[normalize-space(text()) = 'EXCEPTION'])=0"><span class="success">&#x2714;</span></xsl:if>
			</td>
			<td><span class="clickable">
			        <xsl:attribute name="onclick">ToggleClass('Details_<xsl:value-of select="SuiteName"/>')</xsl:attribute>
					<xsl:attribute name="ondblclick">ToggleClass('Details_<xsl:value-of select="SuiteName"/>')</xsl:attribute>
				<xsl:value-of select="SuiteName"/>
			</span>
			</td>
			</tr>
			<tr><td colspan="2"><table>
				<xsl:attribute name="id">Details_<xsl:value-of select="SuiteName"/></xsl:attribute>
			<xsl:for-each select=".//TestCases/TestCase">
			<tr>
				<td width="15px"></td>
				<td width="15px">
				<xsl:if test="TestResult='EXCEPTION'"><span class="failure">&#x2718;</span></xsl:if>
				<xsl:if test="TestResult='FAIL'"><span class="failure">&#x2718;</span></xsl:if>
				<xsl:if test="TestResult='PASS'"><span class="success">&#x2714;</span></xsl:if>
				</td>
				<td><xsl:value-of select="TestName"/>
				</td>
			</tr>
			</xsl:for-each>
			</table></td></tr>
		</xsl:for-each>
		</table></div>
      </td>
    </tr>
  </xsl:template>

</xsl:stylesheet>