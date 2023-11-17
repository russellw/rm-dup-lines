var options = true;
var inplace = false;
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
				Console.WriteLine("rm-dup-lines 1.0");
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

foreach (var file in files) {
	var n = 0;
	var v = new List<string>();
	foreach (var s in File.ReadLines(file)) {
		if (v.Count > 0)
			if (v[v.Count - 1] == s || v.Count > 1 && v[v.Count - 2] == s && v[v.Count - 1] == "") {
				n++;
				continue;
			}
		v.Add(s);
	}
	if (v.Count > 0 && v.Last() == "")
		v.RemoveAt(v.Count - 1);
	if (inplace) {
		if (n == 0)
			continue;
		WriteLines(file, v);
		Console.WriteLine($"{file}\t{n}");
		continue;
	}
	foreach (var s in v)
		Console.WriteLine(s);
}
return 0;

static void Help() {
	Console.WriteLine("Usage: rm-dup-lines [options] file...");
	Console.WriteLine("");
	Console.WriteLine("-h  Show help");
	Console.WriteLine("-V  Show version");
	Console.WriteLine("-i  In-place edit");
}

static void WriteLines(string file, IEnumerable<string> v) {
	using (StreamWriter writer = new StreamWriter(file)) {
		writer.NewLine = "\n";
		foreach (var s in v)
			writer.WriteLine(s);
	}
}