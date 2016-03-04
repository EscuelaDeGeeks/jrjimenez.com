<Query Kind="Statements">
  <Namespace>System.IO</Namespace>
</Query>

//   C:\Code\Personal\EscuelaDeGeeks\jrjimenez.com\nivel-secundario.1(177):<h3><a rel="bookmark" href="nivel-secundario/143/caracteristicas-y-funcionamiento.html"">Características y&#160;Funcionamiento</a></h3>
var baseDir = @"C:\Code\Personal\EscuelaDeGeeks\jrjimenez.com";
var regex = new Regex("a rel=\"bookmark\" href=\"(?<path>[^\"]+)", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.ExplicitCapture);

foreach (var file in Directory.EnumerateFiles(baseDir, "*", SearchOption.AllDirectories).Where(f => !f.Contains("\\.git\\")))
{
	try
	{
		// <a rel="bookmark" href="noticias/162/primaria-reunion-de-padres.html""
		var content = File.ReadAllText(file);
		if (regex.IsMatch(content))
		{
			foreach (var match in regex.Matches(content).OfType<Match>())
			{
				match.Groups["path"].Value.Dump();
				var filePath = Path.Combine(Path.GetDirectoryName(file), match.Groups["path"].Value);
				if (File.Exists(filePath))
				{
					File.Move(filePath, filePath + ".html");
				}
				else
				{
					("NOT EXISTS!!! " + filePath).Dump();
				}
			}
			content = regex.Replace(content, "a rel=\"bookmark\" href=\"$1.html\"");
			File.WriteAllText(file, content);
		}
	}
	catch (Exception ex)
	{
		ex.Dump();
	}
}
