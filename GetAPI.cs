using System;
using System.Net.Http;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

class GetAPI
{
    private List<string> name = new List<string>(); // 1
    public List<string> getter_name { get { return name; } set { name = value; } }
    private List<string> km_diameter_min = new List<string>(); // 2
    public List<string> getter_km_diameter_min { get { return km_diameter_min; } set { km_diameter_min = value; } }
    private List<string> km_diameter_max = new List<string>(); // 3
    public List<string> getter_km_diameter_max { get { return km_diameter_max; } set { km_diameter_max = value; } }
    private List<string> firstdate = new List<string>(); // 4
    public List<string> getter_firstdate { get { return firstdate; } set { firstdate = value; } }
    private List<string> lastdate = new List<string>(); // 5
    public List<string> getter_lastdate { get { return lastdate; } set { lastdate = value; } }
    private List<string> absolute_magnitude = new List<string>(); //6
    public List<string> getter_absolute_magnitude { get { return absolute_magnitude; } set { absolute_magnitude = value; } }
    private List<string> orbite_determiantion_date = new List<string>();//7
    public List<string> getter_orbite_determiantion_date { get { return orbite_determiantion_date; } set { orbite_determiantion_date = value; } }
    private List<string> observations_used = new List<string>();//8
    public List<string> getter_observations_used { get { return observations_used; } set { observations_used = value; } }
    private List<string> min_orbit_intersection = new List<string>();//9
    public List<string> getter_min_orbit_intersection { get { return min_orbit_intersection; } set { min_orbit_intersection = value; } }
    private List<string> eccentricity = new List<string>();//10
    public List<string> getter_eccentricity { get { return eccentricity; } set { eccentricity = value; } }
    private List<string> semi_major_axis = new List<string>();//11
    public List<string> getter_semi_major_axis { get { return semi_major_axis; } set { semi_major_axis = value; } }
    private List<string> Oribtal_period = new List<string>();//12
    public List<string> getter_oribtal_period { get { return Oribtal_period; } set { Oribtal_period = value; } }
    private List<string> orbital_class_range = new List<string>();//13
    public List<string> getter_orbital_class_range { get { return orbital_class_range; } set { orbital_class_range = value; } }

    private Dictionary<string, List<string>> closeApproachDates = new Dictionary<string, List<string>>(); // dictionnaire contenant les approches passé et future des astéroides
    public Dictionary<string, List<string>> getter_closeApproachDates { get { return closeApproachDates; } set { closeApproachDates = value; } }
     public GetAPI() // stocker les données qu'on va afficher dans le 'form1.cs'
    {

        var client = new HttpClient(); // on initialize client pour effectuer des requetes http
        var response = client.GetAsync("https://api.nasa.gov/neo/rest/v1/neo/browse?api_key=hKsgu4aBryojpNleHhyOJj2EYBa9jNuAoJ624nfx").Result; // on effectue une requete avec un lien
        var content = response.Content.ReadAsStringAsync().Result; // on stocke le contenu d'une http reponse et on la convertie en string
        var data = JObject.Parse(content); // on initie un Jobject avec le string de la variable "content"
        JArray near_earth_objects = (JArray)data["near_earth_objects"]; // on prend l'arbre 'near_earth_objects
        Dictionary<string, JObject> linksData = new Dictionary<string, JObject>(); // on initie un dictionnaire de string et de Jobject
        foreach (JObject objects in near_earth_objects) // cette boucle nous sert a utiliser le contenu de nearth_earth_objects
        {
            var links = (string)objects["links"]["self"]; // on stocke la partie links self ex : http://api.nasa.gov/neo/rest/v1/neo/2000433?api_key=hKsgu4aBryojpNleHhyOJj2EYBa9jNuAoJ624nfx
            var client1 = new HttpClient(); // on initialize un nouveau client pour effectuer des requetes http
            var response_each_links = client1.GetAsync(links).Result; // on fait de meme avec chaque liens dans links self
            var content_each_links = response_each_links.Content.ReadAsStringAsync().Result;
            var data_each_links = JObject.Parse(content_each_links);
            linksData.Add(links, data_each_links); // on ajoute le contenu de tout les liens recoltés dans le dictionnaire sous cette form string : links, data_each_links : Jobject
        }
        foreach (var item in linksData)
        {// on stocke les différents éléments des liens dans des variables de types string (leur nom decris leur contenus)
            string kilometers_estimated_diameter_min = (string)item.Value["estimated_diameter"]["kilometers"]["estimated_diameter_min"];
            string kilometers_estimated_diameter_max = (string)item.Value["estimated_diameter"]["kilometers"]["estimated_diameter_max"];
            string first_date = (string)item.Value["orbital_data"]["first_observation_date"].ToString();
            string last_date = (string)item.Value["orbital_data"]["last_observation_date"].ToString();
            string nom = (string)item.Value["name"];
            string absolute_magnitude_h = (string)item.Value["absolute_magnitude_h"];
            string orbit_determination_date = (string)item.Value["orbital_data"]["orbit_determination_date"].ToString();
            string observations_use = (string)item.Value["orbital_data"]["observations_used"].ToString();
            string minimum_orbit_intersection = (string)item.Value["orbital_data"]["minimum_orbit_intersection"].ToString();
            string ecentricity = (string)item.Value["orbital_data"]["eccentricity"].ToString();
            string semi_major_x = (string)item.Value["orbital_data"]["semi_major_axis"].ToString();
            string orbital_period = (string)item.Value["orbital_data"]["orbital_period"].ToString();
            string orbit_class_range = (string)item.Value["orbital_data"]["orbit_class"]["orbit_class_range"].ToString();


            // On ajoute a chaque liste de donné ces variables respectives

            name.Add(nom);
            km_diameter_min.Add(kilometers_estimated_diameter_min);
            km_diameter_max.Add(kilometers_estimated_diameter_max);
            firstdate.Add(first_date);
            lastdate.Add(last_date);
            absolute_magnitude.Add(absolute_magnitude_h);
            orbite_determiantion_date.Add(orbit_determination_date);
            observations_used.Add(observations_use);
            min_orbit_intersection.Add(minimum_orbit_intersection);
            eccentricity.Add(ecentricity);
            semi_major_axis.Add(semi_major_x);
            Oribtal_period.Add(orbital_period);
            orbital_class_range.Add(orbit_class_range);
        }

         // on fait une autre variable string pour stocker les donnée du lien de l'apod
        string json = response.Content.ReadAsStringAsync().Result;
        dynamic jsonObject = JsonConvert.DeserializeObject(json);
        foreach (var neo in jsonObject.near_earth_objects)
        {

            string neoName = (string)neo.name; // on prend le nom de chauqe orbite
            foreach (var approach in neo.close_approach_data) // dans la liste close_approach_datta
            {
                string dates = approach.close_approach_date; // date
                string distance = approach.miss_distance.miles; //distance
                string miles = approach.relative_velocity.miles_per_hour;// date 
                string result = dates + " - " + distance + " - " + miles; // on stocke le tout dans une variable resulte
                if (!closeApproachDates.ContainsKey(neoName)) // pr éviter certe erreurs on vérifie ci la liste contient un string key du nom de l'asteroide qui est dans la boucle foreach "neoName"
                {
                    closeApproachDates.Add(neoName, new List<string>()); // ci dans la liste aucun string key ne contient le nom de l'actuel asteroide on la crée dans la liste (au cas ou des asteroides s'ajoutent dans l'api)

                }

                closeApproachDates[neoName].Add(result.ToString()); // puis on ajoute dans la liste lié au string key string key

            }
        }
    }
}
    