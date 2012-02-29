require 'albacore'

desc "Download Dependencies"
task :deps do
	FileList["**/packages.config"].each { |filepath|
	      sh "#{FileList['lib/**/NuGet.exe'].first} i #{filepath} -o packages" }
end

task :default => ["deps"]
