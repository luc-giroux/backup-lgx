var hourWeek, hourDayFrom, hourDayTo, startdate, preStt, JJoin, WeekNo, YearNo, company;
var iChars = "*|\"<>[]{}`\';@$#%\\";

/*function passvalue(val, sdate, preStatus, JustJoin, wkno, yrno){
	hourWeek = val;
	hourDayFrom = hourWeek / 5;
	hourDayTo = hourDayFrom + 4;
	startdate = sdate;
	preStt = preStatus;
	JJoin = JustJoin;
	WeekNo = wkno;
	YearNo = yrno;
	company = ;
}*/

function ShowRejectedTable() {
	document.getElementById("RejectedTable").style.visibility = "visible"
}

function ShowApprovedTable() {
	document.getElementById("ApprovedTable").style.visibility = "visible"
}

/* added by Lawrence Ng (15 Jan 2008) */
function checkZeroHours() {
	if (document.getElementById("cb_SubmitNoManHours").checked == true) {
		document.getElementById("btn_Submit").disabled = true;
		document.getElementById("btn_Save").disabled = true;	
		document.Form1.lbl_btnmsg.value = "I would like to submit timesheet with no man-hours. I understand that after I press the \"Submit With No Man-Hours\" button, it cannot submit again."	
	}
}
/* end - added by Lawrence Ng (15 Jan 2008) */
 
function ChangeUpperCase(ind) {
    var tempdesc = eval("document.Form1.ccdesc" + ind);    

    for (var i = 0; i < tempdesc.value.length; i++) {
	if (iChars.indexOf(tempdesc.value.charAt(i)) != -1){
		alert ("Task Description contains illegal characters!\n * | \" < > [ ] { } ` \' ; @ $ # % \\ "); 
		tempdesc.focus(); 
		return false;
	}
    }
    tempdesc.value = tempdesc.value.toUpperCase();
}

function ChangeCTRCodeUpperCase(ind) {
    var tempctr = eval("document.Form1.ctrc" + ind);    

    for (var i = 0; i < tempctr.length; i++) {
	if (iChars.indexOf(tempctr.charAt(i)) != -1){
		alert ("CTR/activity code contains illegal characters!\n * | \" < > [ ] { } ` \' ; @ $ # % \\ "); 
		tempctr.focus(); 
		return false;
	}
    }
    tempctr.value = tempctr.value.toUpperCase();
}

function validateNTfield(field, performValidation)
    {
    if (field.value == "")
        {
        return 0;
        }
    if (isNaN(field.value)) 
        {
		alert("Number only.");
		field.value = "";
		field.focus();
		return 0;
		}
    if ( (field.value.indexOf("-") != -1) && (performValidation == true) )
	    {
		alert("Number must only contain positive values!");
		field.value = "";
		field.focus();
		return 0;
		}
	if( (!isNaN(parseFloat(field.value))) && (performValidation == true) )
	    {
		if (parseFloat(field.value) > parseFloat(hourDayTo)) 
		    {
			alert("Normal time hours worked per day should not be more than "+hourDayTo+" hours!");
			field.value = "";
			field.focus();
			}
		}
	/* if all field validations passed return the float value */
	return parseFloat(field.value);
    } /* validateNTfield() */

function validateOTfield(field, normalhours, performValidation)
    {
    var result;
    if (field.value == "")
        {
        return 0;
        }

    if (isNaN(field.value)) 
        {
		alert("Number only.");
		field.value = "";
		field.focus();
		return 0;
		}
    if ( (field.value.indexOf("-") != -1) && (performValidation == true) ) 
	    {
		alert("Number must only contain positive values!");
		field.value = "";
		field.focus();
		return 0;
		}
	if( (!isNaN(parseFloat(field.value))) && (performValidation == true) ) 
	    {
		if (parseFloat(field.value) > 24) 
		    {
			alert("Over time hours worked per day should not be more than 24 hours!");
			field.value = "";
			field.focus();
			}
		result = parseFloat(field.value);
		if ((result + normalhours) > 24) {
					alert("Total hours per day should not be more than 24 hours!");				
					field.value = "";				
				}
		}
	/* if all field validations passed return the float value */
	return parseFloat(field.value);
    } /* validateOTfield() */

function changevalue() 
    {
    var fieldNT1, fieldNT2, fieldNT3, fieldNT4, fieldNT5, fieldNT6, fieldNT7, fieldNTTotal;
    var hoursNT1 = 0.00, hoursNT2 = 0.00, hoursNT3 = 0.00, hoursNT4 = 0.00, hoursNT5 = 0.00, hoursNT6 = 0.00, hoursNT7 = 0.00, hoursNTTotal = 0.00;

    var fieldOT1, fieldOT2, fieldOT3, fieldOT4, fieldOT5, fieldOT6, fieldOT7, fieldOTTotal;
    var hoursOT1 = 0.00, hoursOT2 = 0.00, hoursOT3 = 0.00, hoursOT4 = 0.00, hoursOT5 = 0.00, hoursOT6 = 0.00, hoursOT7 = 0.00, hoursOTTotal = 0.00;
    var totalHours = 0.00;

    /* for total line update */
    var totalHoursNT1 = 0.00, totalHoursNT2 = 0.00, totalHoursNT3 = 0.00, totalHoursNT4 = 0.00, totalHoursNT5 = 0.00, totalHoursNT6 = 0.00, totalHoursNT7 = 0.00, totalHoursNTTotal = 0.00;
    var totalHoursOT1 = 0.00, totalHoursOT2 = 0.00, totalHoursOT3 = 0.00, totalHoursOT4 = 0.00, totalHoursOT5 = 0.00, totalHoursOT6 = 0.00, totalHoursOT7 = 0.00, totalHoursOTTotal = 0.00;
    
    var fieldCostCode, fieldCtrCode;

    var rowCount;
    var performLineValidations = true; // enable the validation at the end - previously perfomed in checksum function
    var performGlobalValidations = true; // enable the validation at the end - previously perfomed in checksum function
    
    if (document.all.rb_Default.disabled == true)
        {
        performLineValidations = false;
        }
        
    if (document.all.radio2delete == undefined) // no rows
        {
        rowCount = 0; // will bypass the other validations, but update the totals
        }
    else
        {
        if (document.all.radio2delete.length == undefined) // there is only one row
            {
            rowCount = 1; // note that this will have to be treated as a special case in the loop below
            }
        else
            {
            rowCount = document.all.radio2delete.length;
            }
        }

    var itemID;
    for (var i=0; i < rowCount; i++) 
        {
        if (rowCount == 1)
            {
            // depending of the sequence, we may have a 1 item array or a single value
            if (document.all.radio2delete.value == undefined)
                {
                itemID = document.all.radio2delete(i).value;
                }
            else
                {
                itemID = document.all.radio2delete.value;
                }
            }
        else
            {
            itemID = document.all.radio2delete(i).value;
            }
        fieldCostCode = eval("document.Form1.ccode" + itemID);
        fieldCtrCode = eval("document.Form1.ctrc" + itemID);

        fieldNT1 = eval("document.Form1.ntsa" + itemID);
        fieldNT2 = eval("document.Form1.ntsu" + itemID);
        fieldNT3 = eval("document.Form1.ntmo" + itemID);
        fieldNT4 = eval("document.Form1.nttu" + itemID);
        fieldNT5 = eval("document.Form1.ntwe" + itemID);
        fieldNT6 = eval("document.Form1.ntth" + itemID);
        fieldNT7 = eval("document.Form1.ntfr" + itemID);
        fieldOT1 = eval("document.Form1.otsa" + itemID);
        fieldOT2 = eval("document.Form1.otsu" + itemID);
        fieldOT3 = eval("document.Form1.otmo" + itemID);
        fieldOT4 = eval("document.Form1.ottu" + itemID);
        fieldOT5 = eval("document.Form1.otwe" + itemID);
        fieldOT6 = eval("document.Form1.otth" + itemID);
        fieldOT7 = eval("document.Form1.otfr" + itemID);
        fieldNTTotal = eval("document.Form1.ntto" + itemID);
        fieldOTTotal = eval("document.Form1.otto" + itemID);

        fieldCostCode.value = fieldCostCode.value.replace(/^\s*|\s*$/g,"");
        fieldCtrCode.value = fieldCtrCode.value.replace(/^\s*|\s*$/g,"");
        fieldNT1.value = fieldNT1.value.replace(/^\s*|\s*$/g,"");
        fieldNT2.value = fieldNT2.value.replace(/^\s*|\s*$/g,"");
        fieldNT3.value = fieldNT3.value.replace(/^\s*|\s*$/g,"");
        fieldNT4.value = fieldNT4.value.replace(/^\s*|\s*$/g,"");
        fieldNT5.value = fieldNT5.value.replace(/^\s*|\s*$/g,"");
        fieldNT6.value = fieldNT6.value.replace(/^\s*|\s*$/g,"");
        fieldNT7.value = fieldNT7.value.replace(/^\s*|\s*$/g,"");
        fieldOT1.value = fieldOT1.value.replace(/^\s*|\s*$/g,"");
        fieldOT2.value = fieldOT2.value.replace(/^\s*|\s*$/g,"");
        fieldOT3.value = fieldOT3.value.replace(/^\s*|\s*$/g,"");
        fieldOT4.value = fieldOT4.value.replace(/^\s*|\s*$/g,"");
        fieldOT5.value = fieldOT5.value.replace(/^\s*|\s*$/g,"");
        fieldOT6.value = fieldOT6.value.replace(/^\s*|\s*$/g,"");
        fieldOT7.value = fieldOT7.value.replace(/^\s*|\s*$/g,"");

	    //saturday - nt
	    hoursNT1 = validateNTfield(fieldNT1, performLineValidations)
		//sunday - nt
	    hoursNT2 = validateNTfield(fieldNT2, performLineValidations)
		//monday - nt
	    hoursNT3 = validateNTfield(fieldNT3, performLineValidations)
		//tueday - nt
	    hoursNT4 = validateNTfield(fieldNT4, performLineValidations)
		//wednesday - nt
	    hoursNT5 = validateNTfield(fieldNT5, performLineValidations)
	    //thursday - nt
	    hoursNT6 = validateNTfield(fieldNT6, performLineValidations)
		//friday - nt
	    hoursNT7 = validateNTfield(fieldNT7, performLineValidations)
		//saturday - ot
		hoursOT1 = validateOTfield(fieldOT1, hoursNT1, performLineValidations)
		//sunday - ot
		hoursOT2 = validateOTfield(fieldOT2, hoursNT2, performLineValidations)
		//monday - ot
		hoursOT3 = validateOTfield(fieldOT3, hoursNT3, performLineValidations)
		//tueday - ot
		hoursOT4 = validateOTfield(fieldOT4, hoursNT4, performLineValidations)
	    //wednesday - ot
		hoursOT5 = validateOTfield(fieldOT5, hoursNT5, performLineValidations)
	    //thursday - ot
		hoursOT6 = validateOTfield(fieldOT6, hoursNT6, performLineValidations)
		//friday - ot
		hoursOT7 = validateOTfield(fieldOT7, hoursNT7, performLineValidations)

		fieldNTTotal.value = parseFloat(hoursNT1) + parseFloat(hoursNT2) + parseFloat(hoursNT3) + parseFloat(hoursNT4) + parseFloat(hoursNT5) + parseFloat(hoursNT6) + parseFloat(hoursNT7);
		fieldOTTotal.value = parseFloat(hoursOT1) + parseFloat(hoursOT2) + parseFloat(hoursOT3) + parseFloat(hoursOT4) + parseFloat(hoursOT5) + parseFloat(hoursOT6) + parseFloat(hoursOT7);
		totalHours = parseFloat(fieldNTTotal.value) + parseFloat(fieldOTTotal.value)
		
		totalHoursNT1 += hoursNT1;
		totalHoursNT2 += hoursNT2;
		totalHoursNT3 += hoursNT3;
		totalHoursNT4 += hoursNT4;
		totalHoursNT5 += hoursNT5;
		totalHoursNT6 += hoursNT6;
		totalHoursNT7 += hoursNT7;
		totalHoursOT1 += hoursOT1;
		totalHoursOT2 += hoursOT2;
		totalHoursOT3 += hoursOT3;
		totalHoursOT4 += hoursOT4;
		totalHoursOT5 += hoursOT5;
		totalHoursOT6 += hoursOT6;
		totalHoursOT7 += hoursOT7;

        /* row validations */		
		if (fieldCostCode.value=="" && performLineValidations == true)
		    {
			// alert("Please ensure every row contains cost code and hours.");
			document.Form1.lbl_btnmsg.value = "Please ensure every row contains a cost code.";
			document.getElementById("btn_Submit").disabled = true;
			document.getElementById("btn_Save").disabled = true;
			performLineValidations == false
			performGlobalValidations = false
			}

		if (fieldCtrCode.value=="" && performLineValidations == true) 
		    {
			// alert("Please ensure the cost code ("+fieldCostCode.value+") contains CTR/Activity code.");
			document.Form1.lbl_btnmsg.value = "Please ensure the cost code ("+fieldCostCode.value+") contains CTR/Activity code.";
			document.getElementById("btn_Submit").disabled = true;
			document.getElementById("btn_Save").disabled = true;
			performLineValidations == false
			performGlobalValidations = false
			}
        if (parseFloat(totalHours) == 0 && performLineValidations == true)
            {
			document.Form1.lbl_btnmsg.value = "Please ensure every row contains hours or remove the unused rows.";
			document.getElementById("btn_Submit").disabled = true;
			document.getElementById("btn_Save").disabled = true;
			performLineValidations == false
			performGlobalValidations = false
            }
	} // end for loop

    // update the total line
    document.Form1.ntsato.value = parseFloat(totalHoursNT1);
    document.Form1.ntsuto.value = parseFloat(totalHoursNT2);
    document.Form1.ntmoto.value = parseFloat(totalHoursNT3);
    document.Form1.nttuto.value = parseFloat(totalHoursNT4);
    document.Form1.ntweto.value = parseFloat(totalHoursNT5);
    document.Form1.ntthto.value = parseFloat(totalHoursNT6);
    document.Form1.ntfrto.value = parseFloat(totalHoursNT7);
    document.Form1.nttoto.value = parseFloat(totalHoursNT1) + parseFloat(totalHoursNT2) + parseFloat(totalHoursNT3) + parseFloat(totalHoursNT4) + parseFloat(totalHoursNT5) + parseFloat(totalHoursNT6) + parseFloat(totalHoursNT7);
    document.Form1.otsato.value = parseFloat(totalHoursOT1);
    document.Form1.otsuto.value = parseFloat(totalHoursOT2);
    document.Form1.otmoto.value = parseFloat(totalHoursOT3);
    document.Form1.ottuto.value = parseFloat(totalHoursOT4);
    document.Form1.otweto.value = parseFloat(totalHoursOT5);
    document.Form1.otthto.value = parseFloat(totalHoursOT6);
    document.Form1.otfrto.value = parseFloat(totalHoursOT7);
    document.Form1.ottoto.value = parseFloat(totalHoursOT1) + parseFloat(totalHoursOT2) + parseFloat(totalHoursOT3) + parseFloat(totalHoursOT4) + parseFloat(totalHoursOT5) + parseFloat(totalHoursOT6) + parseFloat(totalHoursOT7);
	document.Form1.count.value = document.all.project_table.rows.length;
	
	if (performGlobalValidations == true)
        {
    	var hourweek, nttoto, ottoto, hourOT, weekno1;
	    hourweek = parseFloat(hourWeek);
        nttoto = parseFloat(document.Form1.nttoto.value);
	    ottoto = parseFloat(document.Form1.ottoto.value);
	    hourOT = 168 - hourweek;

        if ((nttoto == "0" || nttoto == "")&&(ottoto == "0" || ottoto == "")) 
            {
            document.getElementById("btn_Submit").disabled = true;
            document.getElementById("btn_Save").disabled = true;
            document.Form1.lbl_btnmsg.value = "Please ensure every row contains hours.";
            return;
		    }
        if (nttoto > hourweek) 
            {
            document.getElementById("btn_Submit").disabled = true;
            document.getElementById("btn_Save").disabled = true;
            document.Form1.lbl_btnmsg.value = "Please ensure correct normal time total hours per week.";
            return;
            }
        if (ottoto > hourOT) 
            {
            document.getElementById("btn_Submit").disabled = true;
            document.getElementById("btn_Save").disabled = true;
            document.Form1.lbl_btnmsg.value = "Please ensure correct total over time hours.";
            return;
            }
            /* i.e. all validations passed */
		if (document.getElementById("AddRow").disabled == false)
		    {
			document.getElementById("btn_Submit").disabled = false;
			document.getElementById("btn_Save").disabled = false;
			document.Form1.lbl_btnmsg.value = "By pressing the Submit button, you will not allow to update after. The entry will submit for approval.";	
		    }
	    }
} /* changevalue() */

function openCostCode(numrows) {
	var str;
	str = "ts_costcode.aspx?numrows=";
	str = str + numrows;
	str = str + "&startdate='";
	str = str + startdate + "'";
	str = str + "&company=";
	str = str + eval(document.Form1.hdr_com).value;
	window.open(str,'cal','width=470,height=500,menubar=no,toolbar=no,status=no,scrollbars=no');
}

function openCTRCode(numrows) {
	var str, str2;
	var cc = eval("document.Form1.ccode" + numrows);	
	str2 = cc.value;
	if (str2 == "") {
		alert("Please select your Cost Code first before CTR Code.");
		return false;
	} else {
		str = "ts_ctrcode.aspx?numrows=";
		str = str + numrows;
		str = str + "&CostCode="
		str = str + str2
		str = str + "&Year=";
		str = str + eval(document.Form1.HDRYear).value;
		str = str + "&Week=";
		str = str + eval(document.Form1.hdr_week).value;
		window.open(str,'cal','width=470,height=500,menubar=no,toolbar=no,status=no,scrollbars=no');
	}
}

function openCTRDesc(numrows) {
	var str, str2, str3;
	var cctr = eval("document.Form1.ctrc" + numrows);	
	str2 = cctr.value;
	var cc = eval("document.Form1.ccode" + numrows);	
	str3 = cc.value;
	if ((str2 == "" || str2 == " ") && (str3 == "" || str3 == " ")) {
		alert("Please select both Cost Code and CTR/Activity Code before Task Description.");
		return false;
	} else {
		str = "ts_ctrdesc.aspx?numrows=";
		str = str + numrows;
		str = str + "&startdate='";
		str = str + startdate;
		str = str + "'&costcode=";
		str = str + str3;
		str = str + "&ctrcode=";
		str = str + str2;
		window.open(str,'cal','width=470,height=500,menubar=no,toolbar=no,status=no,scrollbars=no');
	}
}

function getRowIndex(node)
    {
    if (node.nodeName == "TR")
        {
        return node.rowIndex
        }
    else
        {
        return getRowIndex(node.parentNode)
        }    
    }
    
function addrow() {
    /* caro2009 2010-07-12 - add the row at index - 1 because the total row is now part of the table */
    var lastrownum;
    var lastvalue; /* index value for the row fields i.e. ccdesc+value */
    var rowIndex;
    
    if (!(document.all.radio2delete == undefined)) // there is at least one row
        {
        if (document.all.radio2delete.length == undefined) // there is only one row
            {
            lastrownum = getRowIndex(document.all.radio2delete);
            lastvalue = document.all.radio2delete.value;
            }
        else
            {
            for (var i = 0; i < document.all.radio2delete.length; i++)
                {
                rowIndex = getRowIndex(document.all.radio2delete(i).parentNode)
                if (lastrownum == undefined || lastrownum < rowIndex)
                    {
                    lastrownum = rowIndex;
                    }
                if (lastvalue == undefined || lastvalue < document.all.radio2delete(i).value)
                    {
                    lastvalue = document.all.radio2delete(i).value;
                    }
                }
            }
        }
    
    if (lastvalue == undefined) /* i.e. there is no rows */
        {
        lastvalue = 1;
        lastrownum = 2;
        }
    else // we move to the next row
        {
        lastvalue ++;
        lastrownum ++;
        }
    
	var newRow = document.all.project_table.insertRow(lastrownum);
    var name;
    		
    (newRow.insertCell()).innerHTML = "<center><INPUT id='radio2delete' type='radio' name='radio2delete' value='"+lastvalue+"'></center>";
	name = "ccode" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text size=4 maxLength='4' style='FONT-SIZE: xx-small; FONT-FAMILY: Arial; TEXT-ALIGN: center; BORDER-RIGHT: medium none; BORDER-TOP: medium none; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none;' name='"+name+"' id='"+name+"' onmousedown=openCostCode('"+lastvalue+"') readOnly>&nbsp;<IMG alt='Cost Code List' src='images\\list.gif' onclick=openCostCode('"+lastvalue+"')></center>";
	name = "ctrc" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text size=15 maxLength='15' style='FONT-SIZE: xx-small; FONT-FAMILY: Arial; TEXT-ALIGN: center;' name='"+name+"' id='"+name+"' onfocus='this.focus();this.select();' readOnly='true' onblur=ChangeCTRCodeUpperCase('"+lastvalue+"');>&nbsp;<IMG alt='CTR List' src='images\\list.gif' onclick=openCTRCode('"+lastvalue+"')><input type=hidden name='hide"+name+"' id='hide"+name+"' value=''></center>";
	name = "ccdesc" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text size=50 maxLength='150' style='FONT-SIZE: xx-small; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' onfocus='this.focus();this.select();' readOnly='true' onblur=ChangeUpperCase('"+lastvalue+"');><!--&nbsp;<IMG alt='Description List' src='images\\list.gif' onclick=openCTRDesc('"+lastvalue+"')><input type=hidden name='hide"+name+"' id='hide"+name+"' value='UNDEFINED'>--></center>";
	name = "ntsa" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text maxLength='4' size=1 style='text-align: center; FONT-SIZE: xx-small; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' onblur='javascript:changevalue();checkHours(this);' onfocus='this.focus();this.select();'></center>";
	name = "ntsu" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text maxLength='4' size=1 style='text-align: center; FONT-SIZE: xx-small; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' onblur='javascript:changevalue();checkHours(this);' onfocus='this.focus();this.select();'></center>";
	name = "ntmo" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text maxLength='4' size=1 style='text-align: center; FONT-SIZE: xx-small; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' onblur='javascript:changevalue();checkHours(this);' onfocus='this.focus();this.select();'></center>";
	name = "nttu" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text maxLength='4' size=1 style='text-align: center; FONT-SIZE: xx-small; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' onblur='javascript:changevalue();checkHours(this);' onfocus='this.focus();this.select();'></center>";
	name = "ntwe" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text maxLength='4' size=1 style='text-align: center; FONT-SIZE: xx-small; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' onblur='javascript:changevalue();checkHours(this);' onfocus='this.focus();this.select();'></center>";
	name = "ntth" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text maxLength='4' size=1 style='text-align: center; FONT-SIZE: xx-small; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' onblur='javascript:changevalue();checkHours(this);' onfocus='this.focus();this.select();'></center>";
	name = "ntfr" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text maxLength='4' size=1 style='text-align: center; FONT-SIZE: xx-small; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' onblur='javascript:changevalue();checkHours(this);' onfocus='this.focus();this.select();'></center>";
	name = "ntto" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center style='BACKGROUND-COLOR: #ffffcc;'><input type=text size=1 style='BORDER-RIGHT: medium none; BORDER-TOP: medium none; BORDER-LEFT: medium none; BORDER-BOTTOM: medium none; TEXT-ALIGN: center; FONT-SIZE: xx-small; BACKGROUND-COLOR: #ffffcc; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' readOnly></center>";
	name = "otsa" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text maxLength='4' size=1 style='text-align: center; FONT-SIZE: xx-small; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' onblur='javascript:changevalue();checkHours(this);' onfocus='this.focus();this.select();'></center>";
	name = "otsu" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text maxLength='4' size=1 style='text-align: center; FONT-SIZE: xx-small; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' onblur='javascript:changevalue();checkHours(this);' onfocus='this.focus();this.select();'></center>";
	name = "otmo" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text maxLength='4' size=1 style='text-align: center; FONT-SIZE: xx-small; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' onblur='javascript:changevalue();checkHours(this);' onfocus='this.focus();this.select();'></center>";
	name = "ottu" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text maxLength='4' size=1 style='text-align: center; FONT-SIZE: xx-small; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' onblur='javascript:changevalue();checkHours(this);' onfocus='this.focus();this.select();'></center>";
	name = "otwe" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text maxLength='4' size=1 style='text-align: center; FONT-SIZE: xx-small; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' onblur='javascript:changevalue();checkHours(this);' onfocus='this.focus();this.select();'></center>";
	name = "otth" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text maxLength='4' size=1 style='text-align: center; FONT-SIZE: xx-small; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' onblur='javascript:changevalue();checkHours(this);' onfocus='this.focus();this.select();'></center>";
	name = "otfr" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center><input type=text maxLength='4' size=1 style='text-align: center; FONT-SIZE: xx-small; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' onblur='javascript:changevalue();checkHours(this);' onfocus='this.focus();this.select();'></center>";
	name = "otto" + lastvalue;
	(newRow.insertCell()).innerHTML = "<center style='BACKGROUND-COLOR: #ffffcc;'><input type=text size=1 style='BORDER-RIGHT: medium none; BORDER-TOP: medium none; BORDER-LEFT: medium none; BORDER-BOTTOM: medium none; TEXT-ALIGN: center; FONT-SIZE: xx-small; BACKGROUND-COLOR: #ffffcc; FONT-FAMILY: Arial' name='"+name+"' id='"+name+"' readOnly></center>";

	document.Form1.count.value = document.all.project_table.rows.length;
	changevalue();
} /* addrow() */

function deleterow() {
    var rowtodelete;

    if (document.all.radio2delete == undefined) //there is now rows
        {
        return;
        }
    if (document.all.radio2delete.length == undefined) //there is only one row
        {
        if (document.all.radio2delete.checked == true)
            {
            rowtodelete = getRowIndex(document.all.radio2delete.parentNode)
            }
        }
    else
        {
        for (var i = 0; i < document.all.radio2delete.length; i++)
            {
            if (document.all.radio2delete(i).checked == true)
                {
                rowtodelete = getRowIndex(document.all.radio2delete(i).parentNode)
                break;
                }
            }
        }
    if (rowtodelete != undefined)
        {
        project_table.deleteRow(rowtodelete);
        changevalue();
        document.Form1.count.value = document.all.project_table.rows.length;
        }
} /* deleterow() */

function makeReadOnly()
{
    document.getElementById("AddRow").disabled = true;
	document.getElementById("RemoveRow").disabled = true;
	document.getElementById("btn_Save").disabled = true;
	document.getElementById("btn_Submit").disabled = true;
}

/*
 * LGIROUX
 * The maximum of worked hours per day can't exceed 12 Hours.
 * (We don't check that the value entered is a number cause
 * its already checked by the 'changevalue' js function
 */
function checkHours(input)
{
   if(input.value > 12)
   {
    alert("Number of hours per day can't exceed 12.");
    input.value = "0";
   }
}