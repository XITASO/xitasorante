using System.Reflection;

namespace Infrastructure.Import;

public class SegmentationService {
	
	public Stream GetSegmentData(int fid, long firstIndex, long lastIndex)
	{
		return GetSegmentData(fid, firstIndex, lastIndex, 5000);
	}

	public Stream GetSegmentData(int fid, long x1, long x2, long points)
	{
		String filename;

		try {
			filename = getCsvFileName(fid);
		} catch (Exception ex) {
			throw new ArgumentException("unknown file id");
		}

		long fileLines = GetSampleCountByFileId(fid);

		if (points < 0) {
			throw new ArgumentException("points has to be non-negative");
		}

		// set min value
		if (x1 == 0) {
			x1 = 1;
		}

		// set max value
		if (x2 == 0) {
			x2 = fileLines;
		}

		if (x2 < x1) {
			throw new ArgumentException("x2 is smaller than x1");
		}

		long originalSegmentSize = (x2 - x1 + 1);
		String outputFile;

		if (x1 == 1 && x2 == fileLines && originalSegmentSize <= points) {
			// take original file
			outputFile = filename;
		} else {
			String[] tmp = filename.Split(".");
			String extension = tmp[tmp.Length - 1];
			outputFile = new FileInfo(filename).DirectoryName + Path.DirectorySeparatorChar
					+ Path.GetFileNameWithoutExtension(filename)
					+ points + "_" + x1 + "_" + x2 + "." + extension;
		}

		if (!File.Exists(outputFile)) {
			executeCopyProcess(points, x1, x2, filename, outputFile);
		}

		return File.OpenRead(outputFile);
	}

	private String getCsvFileName(int fileId)
	{
		// when fileId is less than 0
		if (fileId < 0) {
			// throw an exception
			throw new Exception();
		}

		var basePath = Directory.GetParent(Assembly.GetAssembly(typeof(SegmentationService)).Location).FullName;
		return Path.Combine(basePath, fileId + ".csv");
	}

	private void executeCopyProcess(long desiredLineCount, long startLineOffset, long endlineOffset, String sourcePath, String targetPath) {
		using (var inputReader = new StreamReader(sourcePath)) {
			using (var outputWriter = new StreamWriter(targetPath)) {
				// write header
				outputWriter.WriteLine(inputReader.ReadLine());

				long expectedLineCount = endlineOffset - startLineOffset;
				if (expectedLineCount > desiredLineCount) {
					double stepSize = expectedLineCount / (double) desiredLineCount;
					CopyDownSampledSegment(inputReader, outputWriter, startLineOffset, endlineOffset, stepSize);
				} // if
				else {
					CopySegment(inputReader, outputWriter, startLineOffset, endlineOffset);
				} // else
				outputWriter.Flush();
				outputWriter.Close();
			} // using outputWriter
		} // using inputWriter
	} // ExecuteCopyProcess

	private static void CopyDownSampledSegment(TextReader inputReader, TextWriter outputWriter,
			double start, double end, double stepSize)
	{
		double targetOffset = start;

		for (long readLineIndex = 0; targetOffset < end; readLineIndex++) {
			var line = inputReader.ReadLine();
			if (line == null || line.Length == 0) {
				break;
			} // if
			if (readLineIndex >= targetOffset) {
				outputWriter.WriteLine(line);
				targetOffset += stepSize;
			} // if
		} // while
	} // CopySegement

	private static void CopySegment(StreamReader inputReader, StreamWriter outputWriter, long begin,
			long endLineOffset) {
		for (long lineIndex = 0; lineIndex < endLineOffset; lineIndex++) {
			String line = inputReader.ReadLine();
			if (line == null || line.Length == 0) {
				break;
			} // if
			if (lineIndex >= begin) {
				outputWriter.WriteLine(line);
			} // if
		} // for
	} // CopySegemnt

	private long GetSampleCountByFileId(int id) {
		String basePath = Directory.GetParent(
				Assembly.GetAssembly(typeof(SegmentationService)).Location).FullName;
		using (var reader = new StreamReader(Path.Combine(basePath, id + ".csv")))
		{
			int i = 0;
			while (reader.ReadLine() != null)
			{
				i++;
			}
			return i;
		}
	}
}
