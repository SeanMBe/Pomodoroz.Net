require 'albacore'

desc "Download Dependencies"
task :deps do
	FileList["**/packages.config"].each { |filepath|
	      sh "#{FileList['lib/**/NuGet.exe'].first} i #{filepath} -o packages" }
end

desc "Build Solution"
msbuild :build do |msb|
  msb.properties = { :configuration => :Debug }
  msb.targets = [ :Clean, :Build ]
  msb.solution = "#{FileList['**/*.sln'].first}" 
end

task :default => ["deps","build"]
