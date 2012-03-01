require 'albacore'
require 'rake/clean'

CLEAN.include("src/**/bin", "src/**/obj", "test/**/bin", "test/**/obj")
CLOBBER.include('packages/**')

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

desc "Run Unit Tests"
task :unit do
puts 'unit tests'
mspec = FileList["packages/**/mspec-clr4.exe"].first 
puts "#{mspec}"
	FileList["test/**/bin/**/*.test*.dll"].each { |dll|
		sh "\"#{mspec}\" \"#{dll}\""}

end

task :default => ["deps","build","unit"]
