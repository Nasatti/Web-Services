using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows.Forms.VisualStyles;

namespace Milesi_Nasatti_WebServices
{
    public partial class Form1 : Form
    {
        static HttpClient client = new HttpClient();
        commenti album = null;
        public Form1()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void sHOWToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        async void button1_Click(object sender, EventArgs e)
        {
            album = await GetAlbumAsync(url.Text);
            if (scelta.Text == "GET")
            {
                ShowProduct(album);
            }
            else if (scelta.Text == "POST")
            {
                album = new commenti();
                album.postId = int.Parse(txt_id.Text); album.email = txt_email.Text; album.name = txt_name.Text; album.body = txt_body.Text;
                var url = await CreateProductAsync(album);
                MessageBox.Show("Eseguito con successo");

            }
            else if (scelta.Text == "PUT")
            {
                album.id = int.Parse(textBox1.Text);
                await UpdateProductAsync(album);
                MessageBox.Show("Eseguito con successo");
            }
            else if (scelta.Text == "DELETE")
            {
                album.id = int.Parse(textBox1.Text);
                var statusCode = await DeleteProductAsync(album.id + "");
                MessageBox.Show("Eseguito con successo");
            }
        }
        public void ShowProduct(commenti album)
        {
            try
            {
                aoao.Text = album.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static async Task<commenti> GetAlbumAsync(string path)
        {
            commenti product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await JsonSerializer.DeserializeAsync<commenti>(await response.Content.ReadAsStreamAsync());
            }
            return product;
        }

        static async Task<Uri> CreateProductAsync(commenti product)
        {

            HttpResponseMessage response = await client.PostAsJsonAsync(
                "/comments", product);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<commenti> UpdateProductAsync(commenti product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"/comments/{product.id}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = await JsonSerializer.DeserializeAsync<commenti>(await response.Content.ReadAsStreamAsync());
            return product;
        }

        static async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"/comments/{id}");
            return response.StatusCode;
        }

        private void scelta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (scelta.Text == "GET")
            {
                aoao.Visible = true;
            }
            else if (scelta.Text == "POST")
            {
                aoao.Visible = false;
                panel.Visible = true;
                label7.Visible = false;
                textBox1.Visible = false;
            }
            else if (scelta.Text == "PUT")
            {
                aoao.Visible = false;
                panel.Visible = true;
                label7.Visible = true;
                textBox1.Visible = true;
            }
            else if (scelta.Text == "DELETE")
            {
                aoao.Visible = false;
                panel.Visible=false;
                textBox1.Visible = true;
                label7.Visible = true;
            }
        }

        private void aoao_TextChanged(object sender, EventArgs e)
        {

        }
    }
}