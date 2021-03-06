﻿using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace LUTtoUE4
{
	static class FileOpener
	{
		#region Input
		public static string[] OpenLUT()
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			dialog.ValidateNames = true;
			dialog.DefaultExt = ".CUBE";
			dialog.Filter = "LUT (*.CUBE)|*.CUBE|All files (*.*)|*.*";
			dialog.Multiselect = false;
			dialog.Title = "Open 3d lookup table";

			if (dialog.ShowDialog() != true)
			{
				MessageBox.Show("Couldn't find file!");
				return null;
			}

			string[] fileContent = File.ReadAllLines(dialog.FileName);
			if (fileContent == null)
			{
				MessageBox.Show("Couldn't open the file.\nTry closing all other applications.");
				return null;
			}

			if (IsLUT_3D(ref fileContent) == false)
			{
				MessageBox.Show("This file isn't LUT_3D.\nThis program opens only 3D lookup tables.");
				return null;
			}


			return fileContent;
		}

		private static bool IsLUT_3D(ref string[] array)
		{
			return array.Select(s => s.StartsWith("LUT_3D_SIZE")).Any();
		}
		#endregion

		#region Output

		public static string GetImageSaveLocation()
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.DefaultExt = ".png";
			dialog.Filter = "*.png|*.png|All files (*.*)|*.*";
			dialog.Title = "Save LUT as *.png";
			dialog.AddExtension = true;

			if (dialog.ShowDialog() != true)
			{
				MessageBox.Show("Canceled by user.");
				return null;
			}
			if (dialog.FileName == null)
			{
				MessageBox.Show("Error while trying to assing save location.");
				return null;
			}


			return dialog.FileName;
		}

		#endregion


	}
}
