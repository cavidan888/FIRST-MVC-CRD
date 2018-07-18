using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassWorks.Controllers
{
    public class WorkerController : Controller
    {
        // GET: Worker

        //public ActionResult Index()
        //{
        //    return View();

        //}

        //[HttpPost]

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Insert(string ad, string position, int? salary)
        {

            if (!(ad == null || position == null || salary == null))
            {
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=c:\users\p106\source\repos\ClassWorks\ClassWorks\App_Data\Sample.mdf;Integrated Security=True");
                connection.Open();

                var insertQuery = "INSERT INTO Worker(Worker_Name,Worker_Position,Worker_Salary) VALUES('" + ad + "','" + position + "','" + salary + "')";

                SqlCommand command = new SqlCommand(insertQuery, connection);

                command.ExecuteNonQuery();

                connection.Close();
            }

            return View();
        }

        public ActionResult Read()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=c:\users\p106\source\repos\ClassWorks\ClassWorks\App_Data\Sample.mdf;Integrated Security=True");
            connection.Open();

            var selectQuery = "select * from Worker";

            var da = new SqlDataAdapter(selectQuery, connection);

            var ds = new DataSet();

            da.Fill(ds);

            var adList = new List<string>();
            var posList = new List<string>();
            var salaryList = new List<string>();
            var idlist = new List<int>();

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                adList.Add(item["Worker_Name"].ToString());
                posList.Add(item["Worker_Position"].ToString());
                salaryList.Add(item["Worker_Salary"].ToString());
                idlist.Add(Convert.ToInt32(item["Id"]));
            }

            ViewBag.adlist = adList;
            ViewBag.postlist = posList;
            ViewBag.salarylist = salaryList;
            ViewBag.idList = idlist;

            return View();
        }

        public ActionResult Delete(int ?Id)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=c:\users\p106\source\repos\ClassWorks\ClassWorks\App_Data\Sample.mdf;Integrated Security=True");
            connection.Open();

            var deleteQuery = "delete from Worker where Id = "+Id+"";

            SqlCommand command = new SqlCommand(deleteQuery, connection);

            command.ExecuteNonQuery();

            connection.Close();

            return RedirectToAction("Read");
        }
    }
}