<Query Kind="Program">
  <Output>DataGrids</Output>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <NuGetReference>HallLibrary.Extensions</NuGetReference>
  <Namespace>HallLibrary.Extensions</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
</Query>

void Main(string[] args)
{
	if (args == null || args.Length == 0)
	{
		var fd = new OpenFileDialog();
		fd.CheckFileExists = true;
		fd.DefaultExt = "*.xml|XML Files";
		
		var cached = AppDomain.CurrentDomain.GetData(nameof(fd.FileName));
		fd.FileName = (string)cached;
		if (fd.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		AppDomain.CurrentDomain.SetData(nameof(fd.FileName), fd.FileName);
		args = new[] { fd.FileName };
	}
	var xe = XElement.Load(args[0]);
	var parent = xe.Descendants().First(x => !x.HasElements).Parent;
	var dt = XML.ToDataTable(XmlReader.Create(args[0]), parent.Name.LocalName);
	
	if (args.Length == 2)
		dt = dt.Filter(args[1]);
	
	#if (!CMD)
		dt.Dump();
	#else
		var output = Path.ChangeExtension(args[0], "html");
		File.WriteAllText(output, Util.ToHtmlString(dt));
		Process.Start(output);
	#endif
}
