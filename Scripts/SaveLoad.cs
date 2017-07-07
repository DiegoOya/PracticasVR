using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public static class SaveLoad {

	//public static ControlOption options = new ControlOption();

	//public static void Save()
	//{
	//	options.Add(ControlOption.current);
	//	BinaryFormatter bf = new BinaryFormatter();
	//	FileStream file = File.Create("D:/Unity Proyects/Save Data/option.txt");
	//	bf.Serialize(file, SaveLoad.options);
	//	file.Close();
	//}

	//public static void Load()
	//{
	//	if (File.Exists("D:/Unity Proyects/Save Data/option.txt"))
	//	{
	//		BinaryFormatter bf = new BinaryFormatter();
	//		FileStream file = File.Open("D:/Unity Proyects/Save Data/option.txt", FileMode.Open);
	//		options = (List<ControlOption>)bf.Deserialize(file);
	//		file.Close();

	//		ControlOption.current.option = options[0].option;
	//	}
	//}

	public static void Save()
	{
		string data = "Option = " + ControlOption.current.option;
		File.WriteAllText("C:/Users/Polygone/Desktop/Proyectos Unity/Prueba/Combinado Networking/Assets/Ficheros/option.txt", data);
	}

	public static void Load()
	{
		string data = File.ReadAllText("C:/Users/Polygone/Desktop/Proyectos Unity/Prueba/Combinado Networking/Assets/Ficheros/option.txt");
		string[] split = data.Split(' ');
		ControlOption.current.option = Int32.Parse(split[2]);
	}
}
