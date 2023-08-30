global using Microsoft.Toolkit.Uwp.Notifications;
global using System;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.Data;
global using System.Drawing;
global using System.Linq;
global using System.Text;
global using System.Windows.Forms;
global using System.IO;
global using TimeCapture.Lists;
global using CsvHelper;
global using CsvHelper.Configuration;
global using System.Globalization;
global using TimeCapture.utils;
global using Windows.UI.Notifications;
global using Windows.Data.Xml.Dom;
global using System.Windows.Forms.DataVisualization;
global using System.Windows.Forms.DataVisualization.Charting;
global using CsvHelper.Configuration.Attributes;
global using CsvHelper.TypeConversion;
global using Microsoft.SqlServer;
global using Microsoft.SqlServer.Server;
global using Microsoft.Data.SqlClient;
global using Microsoft.Data.Sql;
global using Microsoft.Data.SqlTypes;
global using Xunit;
global using OpenQA.Selenium;
global using OpenQA;
global using OpenQA.Selenium.Chrome;
global using OpenQA.Selenium.Support.UI;
global using System.Threading;
global using TimeCapture.Selenium.utils;
global using TimeCapture.DB;
public static class Setting
{
    public static int True = 1;
    public static int False = 0;
}

public static class TimeFormat
{
    public static int DDMMMYYYY = 1;
    public static int HHMM = 2;
    public static int DDMMMYYYYHHMM = 3;
    public static int HHMMSS = 4;
}

public static class TimeType
{
    public static string General = "General";
    public static string Investigation = "Investigation";
    public static string Report = "Report";
    public static string Bug = "Bug";
    public static string Dev = "Dev,Develop,Development";
    public static string Meetings = "Meeting,Meetings";
    public static string Training = "Training";
    public static string Testing = "Testing,Test";
}

public static class TicketType
{
    public static string NonChargeable = "Non-Chargeable,Non-Chargable";
    public static string Chargeable = "Chargeable,Chargable";
    public static string Support = "Support Contract,Support";
}

public enum LogType
{
    Info,
    Debug,
    Error,
    Warn,
    Fatal,
    Trace
}