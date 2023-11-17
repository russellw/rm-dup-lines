var options = true;
var inplace = true;
var files = new List<string>();
foreach (var arg in args) {
	var s = arg;
	if (options) {
		if (s == "--") {
			options = false;
			continue;
		}
		if (s.StartsWith("-")) {
			if (s.StartsWith("--"))
				s = s[1..];
			switch (s) {
			case "-i":
				inplace = true;
				break;
			case "-?":
			case "-h":
			case "-help":
				Help();
				return 0;
			case "-V":
			case "-v":
			case "-version":
				Console.WriteLine("rm-dup-lines 0.1");
				return 0;
			default:
				Console.WriteLine("{0}: unknown option", arg);
				return 1;
			}
			continue;
		}
	}
	files.Add(s);
}
if (files.Count == 0) {
	Help();
	return 0;
}
return 0;

void Help() {
	Console.WriteLine("Usage: rm-dup-lines [options] file...");
	Console.WriteLine("");
	Console.WriteLine("-h  Show help");
	Console.WriteLine("-V  Show version");
	Console.WriteLine("-i  In-place edit");
}