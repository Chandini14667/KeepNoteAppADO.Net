using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace KeepNoteApp
{
    class Note
    {
        SqlConnection con = new SqlConnection("Server=IN-4W3K9S3; database=KeepNoteApp; User Id=sa; password=Nani falls down@22!@nc");
        DataSet ds = new DataSet();
        public void CreateNotes()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from KeepNote", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            da.Fill(ds);
            var row = ds.Tables[0].NewRow();
            Console.WriteLine("Enter Title");
            string title = Console.ReadLine();
            Console.WriteLine("Enter Description");
            string description = Console.ReadLine();
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            row["Title"] = $"{title}";
            row["Descrip"] = $"{description}";
            row["NDate"] = $"{date}";
            ds.Tables[0].Rows.Add(row);
            da.Update(ds);
            Console.WriteLine("Notes Created Successfully");
        }
        public void ViewNote()
        {
            SqlDataAdapter da1 = new SqlDataAdapter($"select * from KeepNote", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da1);
            da1.Fill(ds);
            Console.WriteLine("Enter Notes Id to view");
            int id = Convert.ToInt16(Console.ReadLine());
            var row = ds.Tables[0].Select($"Id={id}");
            if(row.Length > 0)
            {
                var rows = row[0];
                Console.WriteLine($"{rows["Title"]} | {rows["Descrip"]} | {rows["NDate"]}");
            }
            else
            {
                Console.WriteLine("Notes does not exists");
            }
            da1.Update(ds);

        }

        public void ViewAllNotes()
        {
            SqlDataAdapter da2 = new SqlDataAdapter($"select * from KeepNote", con);
            da2.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables[0].Rows[i][j]} | ");
                }
                Console.WriteLine();
            }
        }

        public void UpdateNote()
        {
            SqlDataAdapter da3 = new SqlDataAdapter($"select * from KeepNote", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da3);
            da3.Fill(ds);
            Console.WriteLine("Enter Notes Id to update: ");
            int id = Convert.ToInt16(Console.ReadLine());
            var row = ds.Tables[0].Select($"Id={id}");
            if (row.Length > 0)
            {
                var rows = row[0];
                Console.WriteLine("Enter Updated Title");
                string title = Console.ReadLine();
                Console.WriteLine("Enter Updated Description");
                string description = Console.ReadLine();
                rows["Title"] = $"{title}";
                rows["Descrip"] = $"{description}";
                da3.Update(ds);
                Console.WriteLine("Notes Updated Successfully");
            }
            else
            {
                Console.WriteLine("Enter Id which exist in the Note");
            }

        }

        public void DeleteNote()
        {
            SqlDataAdapter da4 = new SqlDataAdapter($"select * from KeepNote", con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da4);
            da4.Fill(ds);
            Console.WriteLine("Enter Id to delete:");
            int id = Convert.ToInt16(Console.ReadLine());
            var row = ds.Tables[0].Select($"Id={id}");
            if(row.Length > 0)
            {
                var rows = row[0];
                rows.Delete();
                da4.Update(ds);
                Console.WriteLine("Notes Deleted Successfully");
            }
            else
            {
                Console.WriteLine("Enter Id which exist in the Note");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string res = null;
            do
            {
                Note note = new Note();
                Console.WriteLine("Welcome to notes app");
                Console.WriteLine("1. Create Notes");
                Console.WriteLine("2. View Note by Id");
                Console.WriteLine("3. View All Notes");
                Console.WriteLine("4. Update Notes By Id");
                Console.WriteLine("5. Delete Notes By Id");
                Console.WriteLine("Enter Your choice: ");
                int choice = Convert.ToInt16(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            note.CreateNotes();
                            break;
                        }
                    case 2:
                        {
                            note.ViewNote();
                            break;
                        }
                    case 3:
                        {
                            note.ViewAllNotes();
                            break;
                        }
                    case 4:
                        {
                            note.UpdateNote();
                            break;
                        }
                    case 5:
                        {
                            note.DeleteNote();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong Choice Entered");
                            break;
                        }
                }

                Console.WriteLine("Do you wish to continue? [y/n] ");
                res = Console.ReadLine();
            } while (res.ToLower() == "y");
        }
    
    }
}