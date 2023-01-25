using Newtonsoft.Json.Linq;
using System.Net;
using System.Reflection;
using System;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {

        private static GetAPI data = new GetAPI(); // on instancie toutes les donnée récoltées dans le Getapi.cs
        private List<string> name = data.getter_name;
        private List<string> km_diameter_min = data.getter_km_diameter_min;
        private List<string> km_diameter_max = data.getter_km_diameter_max;
        private List<string> firstdate = data.getter_firstdate;
        private List<string> lastdate = data.getter_lastdate;
        private List<string> absolute_magnitude = data.getter_absolute_magnitude;
        private List<string> orbite_determiantion_date = data.getter_orbite_determiantion_date;
        private List<string> observations_used = data.getter_observations_used;
        private List<string> min_orbit_intersection = data.getter_min_orbit_intersection;
        private List<string> eccentricity = data.getter_eccentricity;
        private List<string> semi_major_axis = data.getter_semi_major_axis;
        private List<string> orbital_period = data.getter_oribtal_period;
        private List<string> orbital_class_range = data.getter_orbital_class_range;
        private Dictionary<string, List<string>> closeApproachDates = data.getter_closeApproachDates;

        private  Form1()
        {
            InitializeComponent(); // on initlialize les composants de notre form (label,button,etc...)
         
            Control(); // debut du form
           Apod(); // image de l'apod

        }

        private void Control()
        {

            Pages(); // on crée les pages

        }

        private void Pages() // on affiche les noms et les firstdates et lastdates des 20 asteroides
        {
            for (int i = 0; i < name.ToString().Length; i++)
            {
                label1.Text = name[0];
                label21.Text = "first : " + firstdate[0] + " last : " + lastdate[0];
                label2.Text = name[1];
                label22.Text = "first : " + firstdate[1] + " last : " + lastdate[1];
                label3.Text = name[2];
                label23.Text = "first : " + firstdate[2] + " last : " + lastdate[2];
                label4.Text = name[3];
                label24.Text = "first : " + firstdate[3] + " last : " + lastdate[3];
                label5.Text = name[4];
                label25.Text = "first : " + firstdate[4] + " last : " + lastdate[4];
                label6.Text = name[5];
                label26.Text = "first : " + firstdate[5] + " last : " + lastdate[5];
                label7.Text = name[6];
                label27.Text = "first : " + firstdate[6] + " last : " + lastdate[6];
                label8.Text = name[7];
                label28.Text = "first : " + firstdate[7] + " last : " + lastdate[7];
                label9.Text = name[8];
                label29.Text = "first : " + firstdate[8] + " last : " + lastdate[8];
                label10.Text = name[9];
                label30.Text = "first : " + firstdate[9] + " last : " + lastdate[9];
                label11.Text = name[10];
                label31.Text = "first : " + firstdate[10] + " last : " + lastdate[10];
                label12.Text = name[11];    
                label32.Text = "first : " + firstdate[11] + " last : " + lastdate[11];
                label13.Text = name[12];
                label33.Text = "first : " + firstdate[12] + " last : " + lastdate[12];
                label14.Text = name[13];
                label34.Text = "first : " + firstdate[13] + " last : " + lastdate[13];
                label15.Text = name[14];   
                label35.Text = "first : " + firstdate[14] + " last : " + lastdate[14];
                label16.Text = name[15];
                label36.Text = "first : " + firstdate[15] + " last : " + lastdate[15];
                label17.Text = name[16];
                label37.Text = "first : " + firstdate[16] + " last : " + lastdate[16];
                label18.Text = name[17];
                label38.Text = "first : " + firstdate[17] + " last : " + lastdate[17];
                label19.Text = name[18];
                label39.Text = "first : " + firstdate[18] + " last : " + lastdate[18];
                label20.Text = name[19];
                label40.Text = "first : " + firstdate[19] + " last : " + lastdate[19];
            }
                
        }

        private async Task Apod()// async pour APOD
        {
            var  client = new HttpClient();
            var url = await client.GetAsync("https://api.nasa.gov/planetary/apod?api_key=hKsgu4aBryojpNleHhyOJj2EYBa9jNuAoJ624nfx"); // la meme chose que pour class getapi.cs mais c'est une fonction async avec des awaits
            var content =  url.Content.ReadAsStringAsync().Result;
            var data = JObject.Parse(content);

            Dictionary<string, JObject> InfoAPOD = new Dictionary<string, JObject>();
            InfoAPOD.Add(data.ToString(), data);
            foreach (var item in InfoAPOD)
            {
                

                string copyright = (string)item.Value["copyright"];
                string url_img = (string)item.Value["url"];
                string date = (string)item.Value["date"];
                string explanation = (string)item.Value["explanation"];
                string title = (string)item.Value["title"];
             
                label44.Text = title;
                label45.Text = "Date : " + date;
                label46.Text = copyright;
                label47.Text = explanation;
                

                using (WebClient client2 = new WebClient())
                {
                    byte[] img = client2.DownloadData("https://apod.nasa.gov/apod/image/2301/RockyRed7_DeepAI_960.jpg");// on telecharge le jpg de l'APOD dans une liste de byte
                    using (MemoryStream mem = new MemoryStream(img))
                    {
                        Image resized = Image.FromStream(mem).GetThumbnailImage(1103, 683, null, IntPtr.Zero); // on la resize pour le fond d'ecran de l'appli
                        Image resized_apod = Image.FromStream(mem).GetThumbnailImage(493, 391, null, IntPtr.Zero);// et pour la page APOD
                        // on change les images
                        pictureBox43.Image = resized_apod; // apod
                        this.BackgroundImage = resized; // fond d'écran
                    }
                }

            }
        }

        private void label1_Click(object sender, EventArgs e) // a chaque click sur un cadre asteroide
        {
            int number = GetNumber(sender); // on prend le nbr contenu dans le "label(int)_click" pour se situer
            Form myForm = CreateForm(number); // on cree un form
        }

        private Form CreateForm (int wich)
        {
            int orbite = wich- 1; // pour se situer et savoir quelle int choisir dans les liste de donnée pour avoir les bonne donnée \ ex : wich = 1 (le label numéro 1) donc on prend la premiere valeur des liste sois name[0] (wich -1))
            Form newForm = new Form();
            newForm.Size = new Size(750, 350);
            newForm.BackColor = Color.DarkGray;
            newForm.BackgroundImageLayout = ImageLayout.Stretch;
            newForm.Text = name[orbite];
            newForm.Load += new EventHandler(NewForm_Load);
            Label label1 = new Label();
            Label label3 = new Label();
            ListBox label2 = new ListBox();
            List<string> dates = closeApproachDates[name[orbite]];
            foreach (string date in dates)
            {
                label2.Items.Add(date); // on ajoute dans une liste box les approches passée et future avec leur vélocité et leur distance
            }
            
            label2.Location = new System.Drawing.Point(400, 50);
            label2.Size = new Size(320, 100);


            label3.Text = "date - miss_distance.miles - relative_velocity.miles_per_hour";
            label3.Font = new Font("Segoe UI", 7);
            label3.ForeColor = Color.Black;
            label3.Size = new Size(900, 200);
            label3.Location = new System.Drawing.Point(450,20);

            label1.Text = // on affiche nos données sur l'astéroide
                "Firstdate : " + firstdate[orbite] + "\n" 
                + "Lastdate : " + lastdate[orbite] + "\n" 
                + "Min Km diameter : " + km_diameter_min[orbite] + "\n"
                + "Max Km diamter : " + km_diameter_max[orbite] + "\n"
                +"Absolute magnitude : " +  absolute_magnitude[orbite] + "\n"
                + "Orbite determination date : " + orbite_determiantion_date[orbite] + "\n"
                + "Observations used : " + observations_used[orbite] + "\n"
                + "Min orbit intersection : " + min_orbit_intersection[orbite] + "\n"
                + "Eccentricity : " + eccentricity[orbite] + "\n"
                + "Semi major axis : " + semi_major_axis[orbite] + "\n"
                + "Orbital class range : " + orbital_class_range[orbite] + "\n"
                + "Orbital period : " + orbital_period[orbite] + "\n"; 
            label1.Font = new Font("Segoe UI", 12);
            label1.ForeColor = Color.White;
            label1.Size = new Size(400, 300);
            label1.Location = new System.Drawing.Point(25, 25);

        
            
            newForm.Controls.Add(label2); // on ajoute les 3 labels au form
            newForm.Controls.Add(label1);
            newForm.Controls.Add(label3);
            newForm.Show(); // on l'affiche
            return newForm;
            
        }

        private void NewForm_Load(object? sender, EventArgs e)
        {
            Form form = (Form)sender; // on load les tt forms

        }

        private int GetNumber(object sender) //On prend l 'int contenu dans les charactères d'un object (un click event d'un label dans ce cas )
        { 
            string labelName = ((Label)sender).Name;
            Match match = Regex.Match(labelName, @"\d+");
            int number = int.Parse(match.Value);
            return number;
        }

        private void label2_Click(object sender, EventArgs e) // on fait ca avec tout les boutons
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm( number);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm( number);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm( number);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm( number);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm( number);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm( number);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm( number);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm( number);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm( number);
        }

        private void label11_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm( number);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm(number);
        }

        private void label13_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm(number);
        }

        private void label14_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm(number);
        }

        private void label15_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm(number);
        }

        private void label16_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm(number);
        }

        private void label17_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm(number);
        }

        private void label18_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm(number);
        }

        private void label19_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm(number);
        }

        private void label20_Click(object sender, EventArgs e)
        {
            int number = GetNumber(sender);
            Form myForm = CreateForm(number);
        }
    } 
}