using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPT1000.Source.Driver;

namespace HPT1000.GUI
{
    public partial class ReleaseNote : Form
    {
        //----------------------------------------------------------------------------------------------------------
        public ReleaseNote()
        {
            InitializeComponent();

            rtBox.SelectionFont = new Font(rtBox.Font, FontStyle.Bold);
            rtBox.AppendText(" RELEASE NOTES" + Environment.NewLine);

            rtBox.SelectionFont = new Font(rtBox.Font, FontStyle.Bold);
            rtBox.AppendText( Environment.NewLine + "    PANEL PROGRAM");

            AddReleaseVersion_1_0_38();
            AddReleaseVersion_1_0_37();
            AddReleaseVersion_1_0_36();
            AddReleaseVersion_1_0_35();
            AddReleaseVersion_1_0_34();
            AddReleaseVersion_1_0_33();
            AddReleaseVersion_1_0_32();
            AddReleaseVersion_1_0_31();
            AddReleaseVersion_1_0_30();
            AddReleaseVersion_1_0_29();
            AddReleaseVersion_1_0_28();
            AddReleaseVersion_1_0_27();
            AddReleaseVersion_1_0_26();
            AddReleaseVersion_1_0_25();
            AddReleaseVersion_1_0_24();
            AddReleaseVersion_1_0_23();
            AddReleaseVersion_1_0_22();
            AddReleaseVersion_1_0_21();
            AddReleaseVersion_1_0_20();
            AddReleaseVersion_1_0_19();
            AddReleaseVersion_1_0_18();
            AddReleaseVersion_1_0_17();
            AddReleaseVersion_1_0_16();
            AddReleaseVersion_1_0_15();
            AddReleaseVersion_1_0_14();
            AddReleaseVersion_1_0_13();
            AddReleaseVersion_1_0_12();
            AddReleaseVersion_1_0_11();
            AddReleaseVersion_1_0_10();
            AddReleaseVersion_1_0_9();
            AddReleaseVersion_1_0_8();
            AddReleaseVersion_1_0_7();
            AddReleaseVersion_1_0_6();
            AddReleaseVersion_1_0_5();
            AddReleaseVersion_1_0_4();
            AddReleaseVersion_1_0_3();
            AddReleaseVersion_1_0_2();
            AddReleaseVersion_1_0_1();

            rtBox.SelectionFont = new Font(rtBox.Font, FontStyle.Bold);
            rtBox.AppendText(Environment.NewLine + "    FIRMWARE");

            AddFirmwareReleaseVersion_1_26();
            AddFirmwareReleaseVersion_1_25();
            AddFirmwareReleaseVersion_1_24();
            AddFirmwareReleaseVersion_1_23();
            AddFirmwareReleaseVersion_1_22();
            AddFirmwareReleaseVersion_1_21();
            AddFirmwareReleaseVersion_1_20();
            AddFirmwareReleaseVersion_1_19();
            AddFirmwareReleaseVersion_1_18();
            AddFirmwareReleaseVersion_1_17();
            AddFirmwareReleaseVersion_1_16();
            AddFirmwareReleaseVersion_1_15();
            AddFirmwareReleaseVersion_1_14();
            AddFirmwareReleaseVersion_1_13();
            AddFirmwareReleaseVersion_1_12();
            AddFirmwareReleaseVersion_1_11();
            AddFirmwareReleaseVersion_1_10();
            AddFirmwareReleaseVersion_1_8();
            AddFirmwareReleaseVersion_1_7();
            AddFirmwareReleaseVersion_1_6();
            AddFirmwareReleaseVersion_1_5();
            AddFirmwareReleaseVersion_1_4();
            AddFirmwareReleaseVersion_1_3();
            AddFirmwareReleaseVersion_1_2();
            AddFirmwareReleaseVersion_1_1();
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_38()
        {
            AddLine("");
            AddVersion("07-12-2022", "1.0.38");

            AddHeading(Types.Heading.Improvment);
            AddLine("PID HV parameters have been hidden");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_37()
        {
            AddLine("");
            AddVersion("26-11-2022", "1.0.37");

            AddHeading(Types.Heading.Bug);
            AddLine("Correct display of negative flow values");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_36()
        {
            AddLine("");
            AddVersion("10-10-2022", "1.0.36");
                        
            AddHeading(Types.Heading.Improvment);
            AddLine("Added the ability to delete archived data");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_35()
        {
            AddLine("");
            AddVersion("24-07-2022", "1.0.35");

            AddHeading(Types.Heading.Bug);
            AddLine("Correcting a bug that caused program stages to be changed");
            AddLine("Allow editing of program parameters for a user with rights, regardless of who was logged in before");

            AddHeading(Types.Heading.Improvment);
            AddLine("Show value of the power supply parameters only for service user");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_34()
        {
            AddLine("");
            AddVersion("04-10-2020", "1.0.34");

            AddHeading(Types.Heading.Improvment);
            AddLine("Clearing previous process information in the text box");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_33()
        {
            AddLine("");
            AddVersion("30-03-2020", "1.0.33");

            AddHeading(Types.Heading.Bug);
            AddLine("Correct reading state of gas program with pressure control");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_32()
        {
            AddLine("");
            AddVersion("09-02-2020", "1.0.32");

            AddHeading(Types.Heading.Feauture);
            AddLine("Added additional process information");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_31()
        {
            AddLine("");
            AddVersion("01-02-2020", "1.0.31");

            AddHeading(Types.Heading.Feauture);
            AddLine("Display selected gas name as parameter group label");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_30()
        {
            AddLine("");
            AddVersion("13-11-2019", "1.0.30");

            AddHeading(Types.Heading.Feauture);
            AddLine("Added possibility to select type of vacuum gauge");
            AddLine("Added configuration parameters to pump protection against chamber leakage");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed incorrect display of subprogram summary of the 'Gas' stage");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_29()
        {
            AddLine("");
            AddVersion("21-05-2019", "1.0.29");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed issue no stored data in archive when conditions has wrong during start process");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_28()
        {
            AddLine("");
            AddVersion("19-04-2019", "1.0.28");

            AddHeading(Types.Heading.Improvment);
            AddLine("Changed messages related with maintenance ");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_27()
        {
            AddLine("");
            AddVersion("17-07-2018", "1.0.27");
                     
            AddHeading(Types.Heading.Bug);
            AddLine("Correctly display of the Y-values of power series for small power values ");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_26()
        {
            AddLine("");
            AddVersion("27-06-2018", "1.0.26");

            AddHeading(Types.Heading.Improvment);
            AddLine("Hide unnecessary settings PID of HV ");

            AddHeading(Types.Heading.Bug);
            AddLine("Correctly generate a report CSV regardless type of separator decimal ");
            AddLine("Correcting error related with access denied during generating report to PDF ");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_25()
        {
            AddLine("");
            AddVersion("30-01-2018", "1.0.25");

            AddHeading(Types.Heading.Improvment);
            AddLine("Show value of power on chart and stored in archiwum ");
            AddLine("Hide unnecessary settings of voltage range control HV power supply ");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_24()
        {
            AddLine("");
            AddVersion("13-12-2017", "1.0.24");

            AddHeading(Types.Heading.Improvment);
            AddLine("Correctly stored order of subprogram in program ");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_23()
        {
            AddLine("");
            AddVersion("12-12-2017", "1.0.23");

            AddHeading(Types.Heading.Feauture);
            AddLine("Added showing message when changes place of subprograms are unsaved ");

            AddHeading(Types.Heading.Improvment);
            AddLine("Prohibiting modification parameters of subprograms by operator ");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_22()
        {
            AddLine("");
            AddVersion("24-11-2017", "1.0.22");

            AddHeading(Types.Heading.Feauture);
            AddLine("Showing setpoint power as percent value on live chart and archive chart ");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_21()
        {
            AddLine("");
            AddVersion("24-11-2017", "1.0.21");

            AddHeading(Types.Heading.Feauture);
            AddLine("Showing power as percent value on live chart and archive chart ");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_20()
        {
            AddLine("");
            AddVersion("23-11-2017", "1.0.20");

            AddHeading(Types.Heading.Feauture);
            AddLine("Added possibility change min - max range for voltage of HV analog output");

        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_19()
        {
            AddLine("");
            AddVersion("22-11-2017", "1.0.19");

            AddHeading(Types.Heading.Feauture);
            AddLine("Added possibility change range voltage of HV analog output");

        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_18()
        {
            AddLine("");
            AddVersion("17-11-2017", "1.0.18");

            AddHeading(Types.Heading.Feauture);
            AddLine("Added possibility to set PID parameters of HV");

        }//----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_17()
        {
            AddLine("");
            AddVersion("03-11-2017", "1.0.17");

            AddHeading(Types.Heading.Improvment);
            AddLine("Improving correct showed error of motor control");

        }   //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_16()
        {
            AddLine("");
            AddVersion("27-10-2017", "1.0.16");

            AddHeading(Types.Heading.Improvment);
            AddLine("Improving vaporiser description");

        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_15()
        {
            AddLine("");
            AddVersion("18-10-2017", "1.0.15");              

            AddHeading(Types.Heading.Improvment);
            AddLine("Improving English communication");
  
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_14()
        {
            AddLine("");
            AddVersion("06-10-2017", "1.0.14");

            AddHeading(Types.Heading.Feauture);
            AddLine("Added special keys for screen keyboard");
            AddLine("Added export report of history process to file csv");
         

            AddHeading(Types.Heading.Improvment);
            AddLine("Update data on chart also in manual mode");
            AddLine("Corrected change gas (correction factor) for MFC");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed error during load history data when no one process is selected");
            AddLine("Invisible no active option \"Acquisition all time \"");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_13()
        {
            AddLine("");
            AddVersion("21-09-2017", "1.0.13");

            AddHeading(Types.Heading.Feauture);
            AddLine("Added export report of history process to file pdf");
                
            AddHeading(Types.Heading.Bug);
            AddLine("Fixed wrong showed history data on motor chart");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_12()
        {
            AddLine("");
            AddVersion("01-09-2017", "1.0.12");

            AddHeading(Types.Heading.Feauture);
            AddLine("Presentation of data in chart as logarithmic value");
            AddLine("Load data from archive in related of process name");
            AddLine("Presentation of process summary from archive");

            AddHeading(Types.Heading.Improvment);
            AddLine("Changed user permissions");
            AddLine("Stored state of valve and motor in archive");
            AddLine("Show the state of the valve and motor in chart as rectangle");
            AddLine("Stored subprogram parameters of process in archive");
            AddLine("Shortened marker of actual value in chart");

        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_11()
        {
            AddLine("");
            AddVersion("08-08-2017", "1.0.11");

            AddHeading(Types.Heading.Feauture);
            AddLine("Presents state of motor and dosing valve on chart");
            AddLine("Added marker of actual value in chart");

            AddHeading(Types.Heading.Improvment);
            AddLine("More comfortable process adding new gas");
            AddLine("Hidden value of voltage and current for user without service privileges");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed issue showing message about changes subprogram in not true situation");
            AddLine("Fixed an issue crashed application during added new gas");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_10()
        {
            AddLine("");
            AddVersion("02-08-2017", "1.0.10");

            AddHeading(Types.Heading.Feauture);
            AddLine("Enables data entry for MFC channels in manual gas control mode");
  
            AddHeading(Types.Heading.Improvment);
            AddLine("Shows percent flow of mfc channel to relative whole flow of gases");

        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_9()
        {
            AddLine("");
            AddVersion("31-07-2017", "1.0.9");

            AddHeading(Types.Heading.Feauture);
            AddLine("Possible configure range data presents on chart");
            AddLine("Possible scrolling series of chart");

            AddHeading(Types.Heading.Improvment);
            AddLine("Added multiple Y axes of chart");
    
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_8()
        {
            AddLine("");
            AddVersion("24-07-2017", "1.0.8");

            AddHeading(Types.Heading.Feauture);
            AddLine("Added keyboard screen");
           
            AddHeading(Types.Heading.Improvment);
            AddLine("Clear unconfirmed errors during clears alerts");
            AddLine("Blocked possibility of controlling the machine by not logged user");
            AddLine("Add editable mode for create process of subprogram");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed an issue incorrectly show panel of motor during creat motor program");  
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_7()
        {
            AddLine("");
            AddVersion("28-06-2017", "1.0.7");

            AddHeading(Types.Heading.Feauture);
            AddLine("Added a splash screen on time of loading the data from the database");
            AddLine("Automatic turn off the HV when switch to automatic mode");

            AddHeading(Types.Heading.Improvment);
            AddLine("Automatically connect with PLC on the last saved parameters irrespective of the type of communication");
            AddLine("Valve and pump control only in manual mode");
            AddLine("Check compatibility application with database");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed an issue read data from plc with correctly synchronization");
            AddLine("Fixed problem of incorrect reading of motor program activity in subprogram");

        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_6()
        {
            AddLine("");
            AddVersion("02-06-2017", "1.0.6");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed an issue with cyclic read data from PLC");

        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_5()
        {
            AddLine("");
            AddVersion("26-05-2017", "1.0.5");

            AddHeading(Types.Heading.Feauture);
            AddLine("Added window of release notes application ");
            AddLine("Added splash screen");

            AddHeading(Types.Heading.Improvment);
            AddLine("Correct translated fields in generator panel");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed an issue connecting to DB without waiting on starting server of DB");

        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_4()
        {
            AddLine("");
            AddVersion("25-05-2017", "1.0.4");

            AddHeading(Types.Heading.Improvment);
            AddLine("Added unit to parameters of limit pressure in service tab");
            AddLine("Saved IP parameters of connection with PLC in database");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed an issue in main window where panels of MFC flicker when pressure mode is automatic");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_3()
        {
            AddLine("");
            AddVersion("24-05-2017", "1.0.3");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed an issue in main window where panels of MFC flicker when pressure mode is automatic");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_2()
        {
            AddLine("");
            AddVersion("23-05-2017", "1.0.2");

            AddHeading(Types.Heading.Feauture);
            AddLine("Changed title of application");
            AddLine("Changed window icon of application");

        }
        //----------------------------------------------------------------------------------------------------------
        void AddReleaseVersion_1_0_1()
        {
            AddLine("");
            AddVersion("21-05-2017", "1.0.1");

            AddHeading(Types.Heading.Improvment);
            AddLine("Add state of PLC program to machine status ");
            AddLine("Show in main window state of \"Interlock HV\" instead of not active \"Cover Protection\"");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed an issue in archive tab where on chart existed not active series");
            AddLine("Fixed an issue in incorrect display of process times depending on system settings ");
            AddLine("Limit the cycle value of vaporiser to the maximum value that a variable can take at the PLC Program");

        }
        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
        //-------------------   FIRMWARE
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_26()
        {
            AddLine("");
            AddVersion("24-07-2027", "1.26");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed lack of data integrity about the currently running stage of program");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_25()
        {
            AddLine("");
            AddVersion("30-03-2020", "1.25");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed a bug where there was a status Stop on the edge of between two steps of program");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_24()
        {
            AddLine("");
            AddVersion("01-02-2020", "1.24");

            AddHeading(Types.Heading.Improvment);
            AddLine("Implemented control of pressure with use baratron gauge");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_23()
        {
            AddLine("");
            AddVersion("13-11-2019", "1.23");

            AddHeading(Types.Heading.Improvment);
            AddLine("Implemented 'Barotron' as an option to choose one of the vacuum meters");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_22()
        {
            AddLine("");
            AddVersion("07-11-2019", "1.22");

            AddHeading(Types.Heading.Improvment);
            AddLine("Implemented safety procedures of pumping  in the event of a leaking chamber");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_21()
        {
            AddLine("");
            AddVersion("13-06-2018", "1.21");

            AddHeading(Types.Heading.Improvment);
            AddLine("Added limitation of power");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_20()
        {
            AddLine("");
            AddVersion("12-06-2018", "1.20");

            AddHeading(Types.Heading.Improvment);
            AddLine("Changed calculation of current current and voltage values according with new characteristics.");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_19()
        {
            AddLine("");
            AddVersion("26-06-2018", "1.19");

            AddHeading(Types.Heading.Improvment);
            AddLine("Changed control of led tower");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed issue display error related to the MFC flow which is not connected to system");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_18()
        {
            AddLine("");
            AddVersion("30-01-2018", "1.18");

            AddHeading(Types.Heading.Improvment);
            AddLine("Implemented control new type of power supply");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_17()
        {
            AddLine("");
            AddVersion("23-11-2017", "1.17");

            AddHeading(Types.Heading.Improvment);
            AddLine("Changed possibility of configuring the analog voltage to control the power supply to range min - max");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_16()
        {
            AddLine("");
            AddVersion("22-11-2017", "1.16");

            AddHeading(Types.Heading.Feauture);
            AddLine("Added possibility of changing the voltage range for the analog output");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_15()
        {
            AddLine("");
            AddVersion("17-11-2017", "1.15");
                  
            AddHeading(Types.Heading.Improvment);
            AddLine("Implement PID regulator to control power of HV power supply");
        } //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_14()
        {
            AddLine("");
            AddVersion("21-10-2017", "1.14");
        
            AddHeading(Types.Heading.Bug);
            AddLine("Fixed issue correct set gas factor for MFC during pressure control");
        } //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_13()
        {
            AddLine("");
            AddVersion("18-10-2017", "1.13");

            AddHeading(Types.Heading.Improvment);
            AddLine("Configure PLC option for boot program from SD card");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed an issue no generating message when error flow range not occurred");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_12()
        {
            AddLine("");
            AddVersion("06-10-2017", "1.12");

            AddHeading(Types.Heading.Improvment);
            AddLine("Added correction factor of MFC ");
            AddLine("Changed purge process (no turned off fore pump)");
            AddLine("Changed protection system by vacuum switch");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_11()
        {
            AddLine("");
            AddVersion("14-08-2017", "1.11");

            AddHeading(Types.Heading.Improvment);
            AddLine("Added the factor to measure gauge");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_10()
        {
            AddLine("");
            AddVersion("09-08-2017", "1.10");

            AddHeading(Types.Heading.Improvment);
            AddLine("Make it possible read state of vaporiser valve by PLC");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_8()
        {
            AddLine("");
            AddVersion("24-07-2017", "1.8");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed an issue too fast checked correctly of PID parameters");
        } 
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_7()
        {
            AddLine("");
            AddVersion("20-07-2017", "1.7");

            AddHeading(Types.Heading.Feauture);
            AddLine("Information user about wrong main supply 3 phase connection");

            AddHeading(Types.Heading.Improvment);
            AddLine("Changged control of motor program. Set time how long motor should be runned");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_6()
        {
            AddLine("");
            AddVersion("20-06-2017", "1.6");
                   
            AddHeading(Types.Heading.Improvment);
            AddLine("Changed logic of control of Main Power Failure signal");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_5()
        {
            AddLine("");
            AddVersion("26-05-2017", "1.5");

            AddHeading(Types.Heading.Feauture);
            AddLine("Show current gas flow setpoint which was set using automatic mode pressure");

            AddHeading(Types.Heading.Improvment);
            AddLine("Changed control power of power supply as percent of value 10V");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed an issue blocked set control value MFC in automatic mode");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_4()
        {
            AddLine("");
            AddVersion("25-05-2017", "1.4");

            AddHeading(Types.Heading.Improvment);
            AddLine("Changed address of inpu signal \"Door_Close\"");

            AddHeading(Types.Heading.Bug);
            AddLine("Fixed an issue incorrectly set control value of MFC in automatic mode pressure");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_3()
        {
            AddLine("");
            AddVersion("23-05-2017", "1.3");

            AddHeading(Types.Heading.Feauture);
            AddLine("Added signal \"Interlock HV\"");

            AddHeading(Types.Heading.Improvment);
            AddLine("Changed logic activation  of signal Door_Open, VacuumSwitch and ThermalSwitch from 1 to 0");
            AddLine("Remove with logic of program not active input \"CoverProtection\" and \"HV_Error_State\"");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_1()
        {
            AddLine("");
            AddVersion("19-05-2017", "1.1");

            AddHeading(Types.Heading.Feauture);
            AddLine("Implement control of machine");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddFirmwareReleaseVersion_1_2()
        {
            AddLine("");
            rtBox.SelectionFont = new Font(rtBox.Font, FontStyle.Bold);
            AddVersion("21-05-2017", "1.2");

            AddHeading(Types.Heading.Feauture);
            AddLine("Adaptation of IO modules to current machine configuration");
        }
        //----------------------------------------------------------------------------------------------------------
        void AddVersion(string date , string ver)
        {
            AddLine("");
            rtBox.SelectionFont = new Font(rtBox.Font, FontStyle.Bold);
            rtBox.AppendText("       " + date + " Version:" + ver + Environment.NewLine);
        }
        //----------------------------------------------------------------------------------------------------------
        void AddHeading(Types.Heading heading)
        {
            AddLine("");
            rtBox.AppendText("          ");
            switch (heading)
            {
                case Types.Heading.Feauture:
                    rtBox.AppendText("Feature" + Environment.NewLine);
                    break;
                case Types.Heading.Improvment:
                    rtBox.AppendText("Improvement" + Environment.NewLine);
                    break;
                case Types.Heading.Bug:
                    rtBox.AppendText("Bug" + Environment.NewLine);
                    break;
            }
        }
        //----------------------------------------------------------------------------------------------------------
        void AddLine(string line)
        {
            if(line != "")
                rtBox.AppendText("             - " + line + Environment.NewLine);
            else
                rtBox.AppendText(Environment.NewLine);
        }
        //----------------------------------------------------------------------------------------------------------
        private void ReleaseNote_Shown(object sender, EventArgs e)
        {
            rtBox.SelectionStart = rtBox.Find(rtBox.Lines[0]);
            rtBox.ScrollToCaret();
        }
        //----------------------------------------------------------------------------------------------------------
    }
}
