using RestSharp;
using RestSharpIntroduction.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestSharpIntroduction
{
    public partial class Form1 : Form
    {
        IRestClient client = new RestClient("http://localhost:32089/");

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            GetAll();
        }



        private void btnGetById_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtPersonId.Text);

            GetById(id);
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            Post();
        }

        private void btnPut_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtPersonId.Text);

            Put(id);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtPersonId.Text);
            Delete(id);
        }

        public void GetAll()
        {
            var request = new RestRequest("api/People", Method.GET);

            var response = client.Execute<List<Person>>(request).Data;



            foreach (Person item in response)
            {
                ListViewItem listViewItems = new ListViewItem(item.PersonId.ToString());

                listViewItems.SubItems.Add(item.FirstName.ToString());
                listViewItems.SubItems.Add(item.LastName.ToString());
                listViewItems.SubItems.Add(item.Sex.ToString());
                listViewItems.SubItems.Add(item.Age.ToString());

                listView.Items.Add(listViewItems);
            }
        }

        public void GetById(int id)
        {
            var request = new RestRequest("api/People/" + id, Method.GET);

            var response = client.Execute<Person>(request).Data;



            ListViewItem listViewItems = new ListViewItem(response.PersonId.ToString());

            listViewItems.SubItems.Add(response.FirstName.ToString());
            listViewItems.SubItems.Add(response.LastName.ToString());
            listViewItems.SubItems.Add(response.Sex.ToString());
            listViewItems.SubItems.Add(response.Age.ToString());

            listView.Items.Add(listViewItems);

        }

        public void Post()
        {
            var request = new RestRequest("api/people", Method.POST);

            //var response = client.Execute<Person>(request).Data;

            request.RequestFormat = DataFormat.Json;
            request.AddBody(new Person
            {
                //PersonId = Convert.ToInt32(txtPersonId.Text),
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Sex = txtSex.Text,
                Age = Convert.ToInt32(txtAge.Text)
            });
            client.Execute(request);
        }

        public void Put(int id)
        {
            var request = new RestRequest("api/people/" + id, Method.PUT);

            //var response = client.Execute<Person>(request).Data;

            request.RequestFormat = DataFormat.Json;
            request.AddBody(new Person
            {
                PersonId = id,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Sex = txtSex.Text,
                Age = Convert.ToInt32(txtAge.Text)
            });
            client.Execute(request);
        }

        public void Delete(int id)
        {
            var request = new RestRequest("api/people/" + id, Method.DELETE);

            //var response = client.Execute<Person>(request).Data;

            request.RequestFormat = DataFormat.Json;
            request.AddBody(new Person
            {
                PersonId = id
            });
            client.Execute(request);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
