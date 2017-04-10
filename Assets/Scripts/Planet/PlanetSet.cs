using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetSet : MonoBehaviour
{
	//private static PlanetSet _instance;		//much better with singletons
	//public static PlanetSet instance {
	//	get {
	//		if (_instance == null)		//This will only happen the first time this reference is used.
	//			_instance = GameObject.FindObjectOfType<PlanetSet> ();
	//		return _instance;
	//	}
	//}

	private const string meshChildPrefix = "Mesh";
	private static string[] planetNames = {
		"Mercury",
		"Venus",
		"Earth",
		"Mars",
		"Jupiter",
		"Saturn",
		"Uranus",
		"Neptune"
	};
	private static string[] planetMaterials = {
		"Materials/MercuryMaterial",
		"Materials/VenusMaterial",
		"Materials/EarthMaterial", 
		"Materials/MarsMaterial",
		"Materials/JupiterMaterial",
		"Materials/SaturnMaterial",
		"Materials/UranusMaterial",
		"NeptuneMaterial"
	};
	private static string[] moonNames = { "Moon" };
	private static string[] moonParentNames = { "Earth" };
	private GameObject rings;
	private float[][] planetParameters = new float[planetNames.Length][];
	private float[][] moonParameters = new float[moonNames.Length][];

	void Awake ()
	{
		//_instance = this;

		float scaleRp = Scales.au2mu;

		//eccentricity, r_pericenter, orbital period, radius, axial tilt, rot period, longtitude of ascending node, mass
		const int numOfPlanetParams = 8;
		planetParameters [0] = new float[numOfPlanetParams] {
			0.2056f,
			0.3075f * scaleRp,
			0.241f,
			29.1654f, //0.38f,
			0.01f,
			-0.16f   ,
			48.3f,
			0.05527f
		};		//Mercury
		planetParameters [1] = new float[numOfPlanetParams] {
			0.0067f,
			0.718f * scaleRp,
			0.615f,
			36.8548f, //0.95f,
			177.4f,
			-0.67f  ,
			76.7f,
			0.8145f
		};		//Venus
		planetParameters [2] = new float[numOfPlanetParams] {
			0.0167f,
			0.9833f * scaleRp,
			1f,
			45f, //1f,
			23.45f,
			-0.0027f,
			349f,
			1f
		};	        //Earth
		planetParameters [3] = new float[numOfPlanetParams] {
			0.0934f,
			1.3814f * scaleRp,
			1.882f,
			27.1836f,//0.53f,
			25.19f,
			-0.0029f,
			49.6f,
			0.1075f
		};		    //Mars
		planetParameters [4] = new float[numOfPlanetParams] {
			0.0488f,
			4.950f * scaleRp/2,
			11.86f ,
			192.880f, //11f,
			3.13f,
			-0.0011f ,
			101f,
			317.828f
		};		//Jupiter
		planetParameters [5] = new float[numOfPlanetParams] {
			0.0542f,
			9.021f * scaleRp/2,
			29.457f ,
			131.998f, //9.14f,
			26.73f,
			-0.0012f,
			114f,
			95.161f
		};		//Saturn
		planetParameters [6] = new float[numOfPlanetParams] {
			0.0472f,
			18.286f * scaleRp/2,
			84.016f ,
			92.3564f, //4f,
			97.77f,
			0.0019f,
			74.2f,
			14.5357f
		};	    //Uranus
		planetParameters [7] = new float[numOfPlanetParams] {
			0.0086f,
			29.81f * scaleRp/2,
			164.791f,
			93.2115f, //3.9f,
			28.32f,
			-0.0018f,
			131f,
			17.1478f
		};		//Neptune

		const int numOfMoonParams = 8;
		moonParameters [0] = new float[numOfMoonParams] {
			0.055f,
			2.44f/2,
			0.074f,
			0.273f,
			6.69f,
			-29 / 365f,
			0.0f,
			0.012f
		};

        //for (int i = 0; i < planetNames.Length; i++)
        switch (gameObject.name)
        {
            case "Mercury": SetPlanet(planetNames[0], planetParameters[0]);break;
            case "Venus": SetPlanet(planetNames[1], planetParameters[1]); break;
            case "Earth": SetPlanet(planetNames[2], planetParameters[2]); break;
            case "Mars": SetPlanet(planetNames[3], planetParameters[3]); break;
            case "Jupiter": SetPlanet(planetNames[4], planetParameters[4]); break;
            case "Saturn": SetPlanet(planetNames[5], planetParameters[5]); break;
            case "Uranus": SetPlanet(planetNames[6], planetParameters[6]); break;
            case "Neptune": SetPlanet(planetNames[7], planetParameters[7]); break;
            case "Moon": SetMoon(moonNames[0], planetNames[2], moonParameters[0]); break;

        }

        //for (int i = 0; i < moonNames.Length; i++)
        //	CreateMoon (moonNames [i], moonParentNames [i], Resources.Load (moonMaterials [i]) as Material, moonParameters [i]);

        //rings = Instantiate (Resources.Load ("Prefabs/Rings") as GameObject) as GameObject;
        //rings.transform.parent = Planet.planetList [5].transform.FindChild (meshChildPrefix + "Saturn");
        //rings.transform.localScale = new Vector3 (5, 5, 5);
    }
    
	void SetPlanet (string name, float[] par)
	{
        GameObject planet = this.gameObject;
        //GameObject son = gameObject.transform.FindChild(this.name+"_P").gameObject;

		//planet.name = name;
		planet.tag = "Planet";

		//planet.layer = 9;

		//AddRigidBody (ref planet, Scales.massScale * par [7]);

		//planet.transform.FindChild ("Planet").name = meshChildPrefix + name;
		//planet.transform.FindChild ("BB").name = "BB" + name;

		planet.transform.localScale = new Vector3 (par [3], par [3], par [3]);

		planet.GetComponent<PlanetaryOrbit> ().Par = par;
		//planet.transform.parent = transform;

		//AddSphereCollider (ref planet, Scales.gravityColliderMult * Scales.massScale * par [7]);
	}

    void SetMoon(string name, string parentName, float[] par)
    {
        GameObject moon = this.gameObject;
        //GameObject moon = Instantiate(Resources.Load("Prefabs/PlanetHead") as GameObject) as GameObject;

        //moon.name = name;
        moon.tag = "Moon";

        //moon.transform.FindChild("Planet").name = meshChildPrefix + name;
        //moon.transform.FindChild("BB").name = "BB" + name;

        moon.transform.localScale = new Vector3(par[3], par[3], par[3]);

        moon.GetComponent<PlanetaryOrbit>().Par = par;

        moon.transform.parent = GameObject.Find(parentName).transform;
    }

    void AddSphereCollider (ref GameObject planet, float radius)
	{
		planet.AddComponent<SphereCollider> ();
		planet.GetComponent<SphereCollider> ().center = Vector3.zero;
		planet.GetComponent<SphereCollider> ().radius = radius;
		planet.GetComponent<Collider> ().isTrigger = true;
	}

	void AddRigidBody (ref GameObject planet, float mass)
	{
		planet.AddComponent<Rigidbody> ();
		planet.GetComponent<Rigidbody> ().useGravity = false;
		planet.GetComponent<Rigidbody> ().angularDrag = 0f;
		planet.GetComponent<Rigidbody> ().mass = mass;
	}
}