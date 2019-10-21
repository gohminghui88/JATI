/*
 * Created by SharpDevelop.
 * User: IRU-OAS
 * Date: 24/07/2017
 * Time: 3:46 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageEditor
{
	/// <summary>
	/// Description of ResultForm.
	/// </summary>
	public partial class ResultForm : Form
	{
		public ResultForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
			
		}
		
		private string strRes;
		private string strResFile;
		
		public string StrRes {
			get { return this.strRes; }
			set {this.strRes = value; }
		}
		
		public string StrResFile {
			get { return this.strResFile; }
			set {this.strResFile = value; }
		}
		void ResultFormLoad(object sender, EventArgs e)
		{
			textBox1.Text = this.StrResFile;
			this.richTextBox1.Text = this.strRes;
		}
		void Button1Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(textBox1.Text);
		}
		
		
		
		
	}
}
