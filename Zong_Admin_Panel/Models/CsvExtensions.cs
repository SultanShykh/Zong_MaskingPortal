using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Zong_Admin_Panel.Models
{
    public class CsvExtensions
    {
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        switch (rows[i].ToString())
                        {
                            case "SMS":
                                {
                                    dr[i] = 1;
                                    break;

                                }
                            case "Call":
                                {
                                    dr[i] = 2;
                                    break;

                                }
                            case "Both":
                                {
                                    dr[i] = 3;
                                    break;

                                }
                            case "KT":
                                {
                                    dr[i] = 1;
                                    break;

                                }
                            case "GPS":
                                {
                                    dr[i] = 2;
                                    break;

                                }
                            case "TRUE":
                                {
                                    dr[i] = 1;
                                    break;

                                }
                            case "FALSE":
                                {
                                    dr[i] = 0;
                                    break;

                                }
                            default:
                                {
                                    dr[i] = rows[i];
                                    break;
                                }

                        }
                    }
                    dt.Rows.Add(dr);
                }

            }


            return dt;
        }

        public static DataTable GetDataFromExcel(string path, int UserId, int RequestedBy , int RequestedTo)
        {
            //Save the uploaded Excel file.
            List<DataTable> dataTables = new List<DataTable>();
            var datalist = new List<dynamic>();
            DataTable dt = new DataTable();
           

            //Open the Excel file using ClosedXML.
            using (XLWorkbook workBook = new XLWorkbook(path))
            {
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);
                string customerId = "";
                //Create a new DataTable.

                //Loop through the Worksheet rows.
                bool firstRow = true;
                foreach (IXLRow row in workSheet.RowsUsed())
                {
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            if (!string.IsNullOrEmpty(cell.Value.ToString()))
                            {
                                dt.Columns.Add(cell.Value.ToString());
                             }
                            else
                            {
                                break;
                            }
                        }
                        dt.Columns.Add("Status");
                        dt.Columns.Add("UserId");
                        dt.Columns.Add("ReuestedBy");
                        dt.Columns.Add("RequestedTo");


                        firstRow = false;
                    }
                    else
                    {
                        int i = 0;
                        DataRow toInsert = dt.NewRow();

                        int j = 0;

                        //var AccountColumn = workSheet.Column(1);
                        //var cityColumn = workSheet.Column(4);
                        //int count = cityColumn.CellsUsed().Count();
                        var c = Color.Red;
                        foreach (IXLCell cell in row.Cells(1, dt.Columns.Count))
                        {
                            try
                            {
                                switch(i)
                                {
                                    
                                        
                                    case 6:
                                        {
                                            toInsert[i] = "2"; // Status

                                            break; 
                                        }

                                    case 7:
                                        {

                                            toInsert[i] = UserId;
                                            break;
                                        }

                                    case 8:
                                        {
                                            toInsert[i] = RequestedBy;
                                            break;
                                        }

                                    case 9:
                                        {
                                            LoginModel val;
                                            val = Cookies.getCookieValue();
                                            if (val.UserType == "2")
                                            {
                                                toInsert[i] = "1";
                                            }
                                            else
                                            {
                                                toInsert[i] = "2";
                                            }
                                            
                                            break;
                                        }
                                    default:
                                        {
                                            if(cell.Value.ToString().Contains("Verified"))
                                            {
                                                toInsert[i] = 1;
                                            }
                                           else if (cell.Value.ToString().Contains("Non Verified"))
                                            {
                                                toInsert[i] = 0;
                                            }
                                           else if (cell.Value.ToString().Contains("OFF NET"))
                                            {
                                                toInsert[i] = 2;
                                            }
                                            else
                                            {

                                                toInsert[i] = cell.Value.ToString();
                                            }
                                            break;
                                        }

                                }
                               
                                
                                
                            }
                            catch (Exception ex)
                            {
                                
                            }
                            i++;
                        }
                      
                   
                  

                        dt.Rows.Add(toInsert);
                        


                    }
                }
              
                return dt;
            }

        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            try
            {


                DataTable dataTable = new DataTable(typeof(T).Name);

                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Defining type of data column gives proper data table 
                    var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                    //Setting column names as Property names

                    dataTable.Columns.Add(prop.Name, type);
                    if (prop.Name == "Attachments")
                    {
                        dataTable.Columns.Add("emailSend");
                    }

                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        if (i == 0)
                        {
                            values[i] = Props[0].GetValue(item, null);

                        }
                        if (i == 8)
                        {
                            values[i] = Props[7].GetValue(item, null);
                        }
                        else if (i == 3)
                        {
                           var tt= values[i] = Props[i].GetValue(item, null);
                            if (values[i].ToString() == "OFF-Net")
                            {
                                values[i] = 1;
                            }
                            else
                            {
                                values[i] = 0;
                            }
                        }
                        else
                        {
                            values[i] = Props[i].GetValue(item, null);
                        }

                    }

                    dataTable.Rows.Add(values);
                }


                //put a breakpoint here and check datatable
                return dataTable;
            }
            catch (Exception ex)
            {

                throw ;
            }
        }
}
}