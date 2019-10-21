/*
 * Created by SharpDevelop.
 * User: IRU-OAS
 * Date: 24/07/2017
 * Time: 1:04 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ImageEditor
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			convertButton.Enabled = false;
			pictureBox1.Enabled = false;
			addButton.Enabled = false;
			delButton.Enabled = false;
			groupBox1.Enabled = false;
		}
		
		
		
		int startX = 0;
		int startY = 0;
		int curX = 0;
		int curY = 0;
		Pen selPen;
	
		void PictureBox1MouseDown(object sender, MouseEventArgs e)
		{
			try {
			
			 if (e.Button == System.Windows.Forms.MouseButtons.Left)
             {
             	Cursor = Cursors.Cross;
                startX = e.X;
                startY = e.Y; 
                
                selPen = new Pen(Color.Red, 1);
              }
              
			 pictureBox1.Refresh();
			}
			
			catch(Exception ex) {
				
			}
		}
		void PictureBox1MouseMove(object sender, MouseEventArgs e)
		{
			try {
			if(e.Button == System.Windows.Forms.MouseButtons.Left) {
				pictureBox1.Refresh();	
				//Cursor = Cursors.Cross;
				curX = e.X;
				curY = e.Y;
				
                Rectangle rect = new Rectangle(startX, startY, curX - startX, curY - startY);
                pictureBox1.CreateGraphics().DrawRectangle(selPen, rect);
                
                
			}
			
			}
			
			catch(Exception ex) {
				
			}
			
		}
		
		//Ref: http://www.c-sharpcorner.com/UploadFile/hirendra_singh/how-to-make-image-editor-tool-in-C-Sharp-cropping-image/
		//Ref: https://stackoverflow.com/questions/34551800/get-the-exact-size-of-the-zoomed-image-inside-the-picturebox
		void PictureBox1MouseUp(object sender, MouseEventArgs e)
		{
			try {
			Cursor = Cursors.Arrow;
			
			//if(image1 != null) image1.Dispose();
					
			//image1 = Image.FromFile(listBox1.SelectedItem.ToString());
					
			//Bitmap croppedBitmap = new Bitmap(image1);
			//croppedBitmap = croppedBitmap.Clone(
            //				new Rectangle(
			//					startX, startY, curX - startX, curY - startY),
            //					image1.PixelFormat);
					
			//croppedBitmap.Save(Directory.GetCurrentDirectory() + "\\temp\\temp" + ".png");
			//pictureBox2.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\temp\\temp" + ".png");
			
			//var wfactor = (double) pictureBox1.Image.Width / pictureBox1.ClientSize.Width;
			//var hfactor = (double) pictureBox1.Image.Height / pictureBox1.ClientSize.Height;
			
			//var resizeFactor = Math.Max(wfactor, hfactor);
			
			Rectangle rect = new Rectangle(startX, startY, curX-startX, curY-startY);
            //First we define a rectangle with the help of already calculated points
            Bitmap OriginalImage = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
            //Original image
            Bitmap _img = new Bitmap(curX-startX, curY-startY);
            // for cropinf image
            Graphics g = Graphics.FromImage(_img);
            // create graphics
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //set image attributes
            g.DrawImage(OriginalImage, 0, 0, rect, GraphicsUnit.Pixel);
 
            pictureBox2.Image = _img;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Width = _img.Width;
           	pictureBox2.Height = _img.Height;
           	
			}
			
			catch(Exception ex) {
				
			}
		}
		void AddButtonClick(object sender, EventArgs e)
		{
			try {
			string selCoordinates = "(" + startX.ToString() + "," + startY.ToString() + "," + curX.ToString() + "," + curY.ToString() + ")";
			listBox2.Items.Add(selCoordinates);
			}
			
			catch(Exception ex) {
					
			}
		}
		void DelButtonClick(object sender, EventArgs e)
		{
			try {
			listBox2.Items.Remove(listBox2.SelectedItem);
			}
			
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
		void Button3Click(object sender, EventArgs e)
		{
			try {
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			
			if(fbd.ShowDialog() == DialogResult.OK)
			{
				dirTextBox.Text = fbd.SelectedPath;
				
				string[] ls = Directory.GetFiles(dirTextBox.Text, "*" + extTextBox.Text);
				
				listBox1.Items.Clear();
				
				int i = 0;
				foreach(string s in ls) {
					
					listBox1.Items.Add(s);
					
					i++;
				}
				
				if(ls.Length > 0) {
					listBox1.SelectedIndex = 0;
					pictureBox1.Image = Image.FromFile(listBox1.Items[0].ToString());
					
					convertButton.Enabled = true;
					pictureBox1.Enabled = true;
					addButton.Enabled = true;
					delButton.Enabled = true;
					groupBox1.Enabled = true;
				}
				
				
			}
			
			}
			
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
		void ListBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			try {
			pictureBox1.Image = Image.FromFile(listBox1.SelectedItem.ToString());
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
		void HelpButtonClick(object sender, EventArgs e)
		{
			try {
				System.Diagnostics.Process.Start("https://github.com/tesseract-ocr/tesseract/wiki/Command-Line-Usage");
			}
			
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
		void ConvertButtonClick(object sender, EventArgs e)
		{
			try {
			if(listBox2.Items.Count == 0) {
				
				if(!allCheckBox.Checked) {
				
					pictureBox1.Image.Save(Directory.GetCurrentDirectory() + "\\JATI\\temp\\temp" + ".png");
					string input = Directory.GetCurrentDirectory() + "\\JATI\\temp\\temp" + ".png";
					string output = Directory.GetCurrentDirectory() + "\\JATI\\temp\\temp" + ".txt";
					Process myProcess = Process.Start(Directory.GetCurrentDirectory() + "\\JATI\\tesseract.exe", "--tessdata-dir ./JATI/ " + input + " " + output.Replace(".txt", "") + " -l " + languageTextBox.Text + " -psm " + psmTextBox.Text);
					myProcess.WaitForExit();
					//MessageBox.Show(Directory.GetCurrentDirectory() + "\\JATI\\tesseract.exe" + "--tessdata-dir ./JATI/ " + input + " " + output.Replace(".txt", "") + " -l " + languageTextBox.Text + " -psm " + psmTextBox.Text);
					
					ResultForm rf = new ResultForm();
					rf.StrResFile = output;
					StreamReader sr = new StreamReader(output);
					rf.StrRes = sr.ReadToEnd();
					sr.Close();
					
					rf.ShowDialog();
				
				}
				
				
				else {
					
					foreach(var i in listBox1.Items) {
						
						string input = i.ToString();
						string output = i.ToString().Replace(extTextBox.Text, ".txt");
						Process myProcess = Process.Start(Directory.GetCurrentDirectory() + "\\JATI\\tesseract.exe", "--tessdata-dir ./JATI/ " + input + " " + output.Replace(".txt", "") + " -l " + languageTextBox.Text + " -psm " + psmTextBox.Text);
						myProcess.WaitForExit();
					
					
						//ResultForm rf = new ResultForm();
						//rf.StrResFile = output;
						//StreamReader sr = new StreamReader(output);
						//rf.StrRes = sr.ReadToEnd();
						//sr.Close();
					
						//rf.ShowDialog();
						
						
						
					}
					
					MessageBox.Show("Completed. ");
				}
			
				
			}
			
			
			else {
				
				if(!allCheckBox.Checked) {
					
					List<string> ls = new List<string>();
					int x = 0;
					foreach(var c in listBox2.Items) {
						string[] coo = c.ToString().Replace("(", "").Replace(")", "").Split(',');
						int s_x = int.Parse(coo[0]);
						int s_y = int.Parse(coo[1]);
						int c_x = int.Parse(coo[2]);
						int c_y = int.Parse(coo[3]);
						
						
						Rectangle rect = new Rectangle(s_x, s_y, c_x-s_x, c_y-s_y);
            			//First we define a rectangle with the help of already calculated points
            			Bitmap OriginalImage = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
            			//Original image
            			Bitmap _img = new Bitmap(c_x-s_x, c_y-s_y);
            			// for cropinf image
            			Graphics g = Graphics.FromImage(_img);
            			// create graphics
            			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            			g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            			g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            			//set image attributes
            			g.DrawImage(OriginalImage, 0, 0, rect, GraphicsUnit.Pixel);
 
            			pictureBox2.Image = _img;
            			pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            			pictureBox2.Width = _img.Width;
           				pictureBox2.Height = _img.Height;
           					
           				pictureBox2.Image.Save(Directory.GetCurrentDirectory() + "\\JATI\\temp\\temp" + x.ToString() + ".png");
           				string input = Directory.GetCurrentDirectory() + "\\JATI\\temp\\temp" + x.ToString() + ".png";
           				string output = Directory.GetCurrentDirectory() + "\\JATI\\temp\\temp" + x.ToString() + ".txt";
						Process myProcess = Process.Start(Directory.GetCurrentDirectory() + "\\JATI\\tesseract.exe", "--tessdata-dir ./JATI/ " + input + " " + output.Replace(".txt", "") + " -l " + languageTextBox.Text + " -psm " + psmTextBox.Text);
						myProcess.WaitForExit();
						
						
						StreamReader sr = new StreamReader(output);
						string line = sr.ReadLine(); string res = line;
						while(line != null) 
						{
							line = sr.ReadLine();
							if(line != "") res += " " + line;
								
						}
						sr.Close();
						ls.Add(res);
					
						x++;
					}
					
					
					string csv = ""; int y = 0;
					foreach(string s in ls) 
					{
						if(y == 0) csv = "\"" + s + "\"";
						else csv += "," + "\"" + s + "\"";
						y++;
					}
					
					StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "\\JATI\\temp\\temp_final" + ".csv");
					sw.WriteLine(csv);
					sw.Close();
					
					ResultForm rf = new ResultForm();
					rf.StrResFile = Directory.GetCurrentDirectory() + "\\JATI\\temp\\temp_final" + ".csv";
					rf.StrRes = csv;
					
					rf.ShowDialog();
				}
				
				
				
				
				else {
					
					List<string> final = new List<string>();
					foreach(var file in listBox1.Items) {
						
						pictureBox1.Image = Image.FromFile(file.ToString());
						
						List<string> ls = new List<string>();
						int x = 0;
						foreach(var c in listBox2.Items) {
							string[] coo = c.ToString().Replace("(", "").Replace(")", "").Split(',');
							int s_x = int.Parse(coo[0]);
							int s_y = int.Parse(coo[1]);
							int c_x = int.Parse(coo[2]);
							int c_y = int.Parse(coo[3]);
						
						
							Rectangle rect = new Rectangle(s_x, s_y, c_x-s_x, c_y-s_y);
            				//First we define a rectangle with the help of already calculated points
            				Bitmap OriginalImage = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
            				//Original image
            				Bitmap _img = new Bitmap(c_x-s_x, c_y-s_y);
            				// for cropinf image
            				Graphics g = Graphics.FromImage(_img);
            				// create graphics
            				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            				g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            				g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            				//set image attributes
            				g.DrawImage(OriginalImage, 0, 0, rect, GraphicsUnit.Pixel);
 
            				pictureBox2.Image = _img;
            				pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            				pictureBox2.Width = _img.Width;
           					pictureBox2.Height = _img.Height;
           					
           					pictureBox2.Image.Save(Directory.GetCurrentDirectory() + "\\JATI\\temp\\temp" + x.ToString() + ".png");
           					string input = Directory.GetCurrentDirectory() + "\\JATI\\temp\\temp" + x.ToString() + ".png";
           					string output = Directory.GetCurrentDirectory() + "\\JATI\\temp\\temp" + x.ToString() + ".txt";
							Process myProcess = Process.Start(Directory.GetCurrentDirectory() + "\\JATI\\tesseract.exe", "--tessdata-dir ./JATI/ " + input + " " + output.Replace(".txt", "") + " -l " + languageTextBox.Text + " -psm " + psmTextBox.Text);
							myProcess.WaitForExit();
						
						
							StreamReader sr = new StreamReader(output);
							string line = sr.ReadLine(); string res = line;
							while(line != null) 
							{
								line = sr.ReadLine();
								if(line != "") res += " " + line;
								
							}
							sr.Close();
							ls.Add(res);
					
							x++;
						}
					
					
						string csv = ""; int y = 0;
						foreach(string s in ls) 
						{
							if(y == 0) csv = "\"" + s + "\"";
							else csv += "," + "\"" + s + "\"";
							y++;
						}
						
						final.Add(csv);
					
					}
					
					StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "\\JATI\\temp\\temp_final" + ".csv");
					
					foreach(string f in final)
						sw.WriteLine(f);
					sw.Close();
					
					int u = 0;
					string finalRes = "";
					foreach (string f in final) {
						
						if(u < 50) finalRes += "\n" + f;
						u++;
					}
					finalRes += "\n...";
					
					ResultForm rf = new ResultForm();
					rf.StrResFile = Directory.GetCurrentDirectory() + "\\JATI\\temp\\temp_final" + ".csv";
					rf.StrRes = finalRes;
					
					rf.ShowDialog();
					
				}
			}
		}
			
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
		void AbtButtonClick(object sender, EventArgs e)
		{
			try {
				AboutForm af = new AboutForm();
				af.ShowDialog();
			}
			
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
		void OldButtonClick(object sender, EventArgs e)
		{
			try {
				System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "\\JATI\\JATI_old.exe");
			}
			
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}
	}
}
