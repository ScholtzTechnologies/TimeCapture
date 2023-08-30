using Uno.Extensions;

namespace TimeCapture.DB
{
    public class Access
    {
        public _logger Logger = new();
        public static String GetConnectionString()
        {
            string dbFile = Path.GetFullPath("../../../DB/Time.mdf");
            return "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbFile + ";Integrated Security=True";
        }

        public void TestConnection(out string Response)
        {
            try
            {
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                connection.Close();
                Response = "Connected - Welcome to TimeCapture";
            }
            catch
            {
                MessageBox.Show("Failed to connect, please ensure the database file has been created and restart the app.");
                Response = "Failed to connect, please correct and restart";
            }
        }

        #region dbAccess
        public DataSet ExecuteQuery(string SQL)
        {
            try
            {
                string connectionString = GetConnectionString();
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(SQL, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                connection.Open();
                adapter.Fill(dataSet);
                connection.Close();
                return dataSet;
            }
            catch (Exception ex)
            {
                Logger.Log(LogType.Error, ex.Message.ToString());
                return new DataSet();
            }
        }

        public void ExecuteNonQuery(string SQL)
        {
            try
            {
                string connectionString = GetConnectionString();
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(SQL, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Logger.Log(LogType.Error, ex.Message.ToString());
            }
}
        #endregion

        #region Get

        public DataSet getsettings()
        {
            string sSQL = @"select ID, Name, Value, Description from Settings";
            return ExecuteQuery(sSQL);
        }

        public DataSet getTickets()
        {
            string sSQL = @"select ID, Name from Tickets";
            return ExecuteQuery(sSQL);
        }

        public DataSet getNotes()
        {
            string sSQL = @"select NoteID, Note, Name, Date, ParentID from Notes";
            return ExecuteQuery(sSQL);
        }

        public DataSet GetNoteByNoteID(int NoteID)
        {
            string sSQL = "select NoteID, Name, Note, Date, ParentID from Notes where NoteID = " + NoteID;
            return ExecuteQuery(sSQL);
        }

        public DataSet GetTasks()
        {
            string sSQL = "select TaskID, Task, Status from Tasks";
            return ExecuteQuery(sSQL);
        }

        public void DeleteTask(int TaskID)
        {
            string sSQL = "Delete From Tasks where TaskID = " + TaskID;
            ExecuteNonQuery(sSQL);
        }

        public void SaveTask(int TaskID, string Name, int Status)
        {
            string sSQL = String.Format(@"if exists (select * from Tasks where TaskID = {0})
	                            update Tasks set Task = '{1}', Status = {2}
		                            where TaskID = {0}
                                else
	                            insert into Tasks (TaskID, Task, Status)
		                            VALUES (
			                            (SELECT ISNULL(MAX(TaskID), 1) + 1 FROM Tasks), '{1}', {2})", 
                                TaskID, Name, Status);
            ExecuteNonQuery(sSQL);
        }

        public DataSet getTime(int type)
        {
            string sSQL = "";
            if (type == 1)
            {
                sSQL = @"select TimeID, Item, TicketNo, Start, [End], Total, TimeType, Description, TicketType, Date from Time";
            }
            else if (type == 2)
            {
                sSQL = @"select TimeID, Item, TicketNo, Start, [End], Total, TimeType, Description, TicketType, Date from Time
                            where ISNULL(IsCaptured,0) = 0";
            }
            else if (type == 3)
            {
                sSQL = @"select TimeID, Item, TicketNo, Start, [End], Total, TimeType, Description, TicketType, Date from Time
                            where ISNULL(IsCaptured,0) = 1";
            }
            return ExecuteQuery(sSQL);
        }

        public void DeleteTime(int iTimeID)
        {
            string sSQL = "delete from Time where TimeID = " + iTimeID;
            ExecuteNonQuery(sSQL);
        }

        public bool UpdateTime(Time time)
        {
            string check = "select 1 from Time where TimeID = " + time.TimeID + " and ISNULL(IsCaptured, 0) = 0";
            if (ExecuteQuery(check).HasRows())
            {
                string sSQL = string.Format(@"update Time set
	                Item = '{1}', TicketNo = '{2}', time.[Start] = '{3}', time.[End] = '{4}', Total = '{5}', 
	                TimeType = '{6}', Description = '{7}', TicketType = '{8}', time.Date = '{9}'
	                where TimeID = {0}"
                    ,time.TimeID, time.Item, time.TicketNo, time.Start, time.End, time.Total, 
                    time.TimeType, time.Description, time.Type, time.Date);
                ExecuteNonQuery(sSQL);
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataSet getTimeByDay(string Date)
        {
            string sSQL = @"select TimeID, Item, TicketNo, Start, [End], Total, TimeType, Description, TicketType, Date from Time
                            where Date = '" + Date + "'";
            return ExecuteQuery(sSQL);
        }

        public DataSet GetTimeByDateRange(string sStart, string sEnd)
        {
            string sSQL;
            if (string.IsNullOrEmpty(sEnd))
            {
                sSQL = String.Format(@"select TimeID, Item, TicketNo, Start, [End], Total, TimeType, Description, TicketType, Date from Time
                            where Date > DATEADD(day,-1,'{0}')", sStart);
            }
            else
            {
                sSQL = String.Format(@"select TimeID, Item, TicketNo, Start, [End], Total, TimeType, Description, TicketType, Date from Time
                            where Date > DATEADD(day,-1,'{0}') and Date < '{1}'", sStart, sEnd);
            }
            return ExecuteQuery(sSQL);
        }
        
        public void getTypes(out DataSet TimeTypes, out DataSet TicketTypes)
        {
            string sqlTime = @"select Name from TimeTypes";
            TimeTypes = ExecuteQuery(sqlTime);

            string sqlTicket = @"select Name, ID from TicketTypes";
            TicketTypes = ExecuteQuery(sqlTicket);
        }

        public bool GetSettingValue(int SettingID)
        {
            string sSQL = "Select ISNULL(Value, 0) as Value from Settings where ID = " + SettingID;
            DataSet ds = ExecuteQuery(sSQL);
            return ds.Tables[0].Rows[0].GetDataRowBoolValue("Value");
        }

        public bool IsBusinessModel()
        {
            string sSQL = "Select Value from Settings where ID = -1";
            return ExecuteQuery(sSQL).Tables[0].Rows[0].GetDataRowBoolValue("Value");
        }

        public string GetSettingsValue(int SettingID)
        {
            string sSQL = "Select Value from Settings where ID = " + SettingID;
            return ExecuteQuery(sSQL).Tables[0].Rows[0].GetDataRowStringValue("Value");
        }

        public DataSet GetTicketTypes()
        {
            string sSQL = "select ID, Name from TicketTypes";
            return ExecuteQuery(sSQL);
        }

        public DataSet GetTickets()
        {
            string sSQL = "Select ID, Name from Tickets";
            return ExecuteQuery(sSQL);
        }

        public DataSet GetTimeToCapture()
        {
            string sSQL = "select * from Time where ISNULL(IsCaptured, 0) = 0";
            return ExecuteQuery(sSQL);
        }

        public void MarkTimeAsCaptured(int TimeID)
        {
            string sSQL = "Update Time set IsCaptured = 1 where TimeID = " + TimeID;
            ExecuteNonQuery(sSQL);
        }

        public string GetUserName()
        {
            string sSQL = "select Value from Settings where ID = 5";
            try
            {
                DataSet ds = ExecuteQuery(sSQL);
                return ds.Tables[0].Rows[0]["Value"].ToString();
            }
            catch { return ""; }
        }

        public string GetPassword()
        {
            string sSQL = "select Value from Settings where ID = 6";
            try
            {
                DataSet ds = ExecuteQuery(sSQL);
                return ds.Tables[0].Rows[0]["Value"].ToString();
            }
            catch { return ""; }
        }

        public bool Login(string Username, string Password, out int UserID)
        {
            string sSQL = "select ID from SystemUsers where LOWER(Name) = '" + Username.ToLower() 
                + "' and Password = '" + Password + "'";
            try
            {
                if (ExecuteQuery(sSQL).HasRows())
                {
                    UserID = ExecuteQuery(sSQL).Tables[0].Rows[0].GetDataRowIntValue("ID");
                    return true;
                }
                else
                {
                    UserID = -1;
                    return false;
                }
            }
            catch { UserID = -1; return false; }
        }

        public bool Register(string Username, string Password, string Email)
        {
            string sSQL = String.Format(@"if not exists (select 1 from SystemUsers where LOWER(SystemUsers.Name) = LOWER('{0}') and LOWER(Email) = LOWER('{2}'))
	            Insert into SystemUsers VALUES
	            (
		            isnull((select MAX(ID) from SystemUsers), 0) + 1,
		            '{0}',
		            '{1}',
		            '{2}',
		            'u'
	            )", Username, Password, Email);
            try
            {
                ExecuteNonQuery(sSQL);
                return true;
            }
            catch { return false; }
        }

        #endregion

        #region Save
        public void saveNote(int NoteID, string Name, string Note, string Date, int ParentID)
        {
            string sSQL;
            if (Note.IsNullOrEmpty())
            {
                sSQL = String.Format(@"IF EXISTS (SELECT * FROM Notes where NoteID = {0})
	                        update Notes
		                        set Name = '{1}', Date = '{2}'
			                        where NoteID = {0}
                            else
	                        insert into Notes (NoteID, Name, Date, ParentID, Note)
		                        VALUES
                                    ((SELECT ISNULL(MAX(NoteID) + 1, 1) from Notes), '{1}', '{2}', {3}, '')",
                                        NoteID, Name, Date, ParentID);
            }
            else
            {
                sSQL = String.Format(@"IF EXISTS (SELECT * FROM Notes where NoteID = {0})
	                        update Notes
		                        set Name = '{1}', Note = '{2}', Date = '{3}'
			                        where NoteID = {0}
                            else
	                        insert into Notes (NoteID, Name, Note, Date, ParentID)
		                        VALUES
                                    ((SELECT ISNULL(MAX(NoteID) + 1, 1) from Notes), '{1}', '{2}', '{3}', {4})",
                                        NoteID, Name, Note.FixString(), Date, ParentID);
            }
            ExecuteNonQuery(sSQL);
        }

        public void DeleteNote(int NoteID)
        {
            string sSQL = String.Format("Delete From Notes where NoteID = {0}", NoteID);
            ExecuteNonQuery(sSQL);
        }

        public void saveTask(int TaskID, string Task, int Status)
        {
            string sSQL;
            string taskCheck = @"select task from Tasks where TaskID = " + TaskID;
            sSQL = String.Format(@"if Exists (select * from Tasks where TaskID = {0})
	                update Tasks set Task = '{1}', Status = '{2}' where TaskID = {0}
                    else
	                insert into Tasks (Task, Status)
		                VALUES ('{1}', '{2}')", TaskID, Task, Status);
            ExecuteNonQuery(sSQL);
        }

        public void SaveTime(int TimeID, string Item, int TicketNo, string Start, string End
            , string Total, string TimeType, string Desc, string TicketType, string Date, int UserID)
        {
            string sSQL = String.Format(@"if Exists (select * from Time where TimeID = {0})
	                update Time set Item = '{1}', TicketNo = {2}, Start = '{3}', [End] = '{4}',
                        Total = '{5}', TimeType = '{6}', Description = '{7}', TicketType = '{8}', Date = '{9}' 
                        where TimeID = {0}
                    else 
                    insert into 
                        Time 
                            (Item, TicketNo, Start, [End],
                            Total, TimeType, Description, TicketType, Date, SystemUserID)
                        VALUES (
                            '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', {10}
                        )", TimeID, Item, TicketNo, Start, End, Total, TimeType, Desc, TicketType, Date, UserID);
            ExecuteNonQuery(sSQL);
            Logger.Log(LogType.Info, "Time Added to DB");
        }

        public void updSettings(int SettingID, int Value, string? sValue)
        {
            string sSQL = "";
            if (Value == -1)
            {
                sSQL = "update Settings set Value = '" + sValue + "' where ID = " + SettingID;
            }
            else
            {
                sSQL = "update Settings set Value = " + Value + " where ID = " + SettingID;
            }
            ExecuteNonQuery(sSQL);
        }

        public void AddTicket(int TicketID, string Name)
        {
            string sSQL = String.Format(@"if not Exists (select * from Tickets where ID = {0})
	            Insert into Tickets Values ({0}, '{1}')", TicketID, Name);
            ExecuteNonQuery(sSQL);
        }

        public void DelTicket(int TicketID)
        {
            string sSQL = "Delete from Tickets where ID = " + TicketID;
            ExecuteNonQuery(sSQL);
        }
        #endregion
    }
}
