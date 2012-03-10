require 'albacore'
require 'rake/clean'

CLEAN.include("src/**/bin", "src/**/obj", "test/**/bin", "test/**/obj")
CLOBBER.include('packages/**')

sln_file = FileList['**/*.sln'].first

desc "Download Dependencies"
task :deps do
	FileList["**/packages.config"].each { |filepath|
	      sh "#{FileList['lib/**/NuGet.exe'].first} i #{filepath} -o packages" }
end

desc "Build Solution"
msbuild :build do |msb|
  msb.properties = { :configuration => :Debug }
  msb.targets = [ :Clean, :Build ]
  msb.solution = "#{sln_file}" 
end

desc "Run Unit Tests"
task :unit do
puts 'unit tests'
mspec = FileList["packages/**/mspec-clr4.exe"].first 
puts "#{mspec}"
	FileList["test/**/bin/**/*.test*.dll"].each { |dll|
		sh "\"#{mspec}\" \"#{dll}\""}

end

desc "All tests"
task :tests => :unit do
end

desc "Publishes web application"
msbuild :publish=>:tests do |m|
    m.properties={:configuration=>:Release}
    m.targets [:_WPPCopyWebApplication]
    m.properties={
         :webprojectoutputdire=>"c:/temp/outputdir/",
	 :outdir=>"c:/temp/outputdir/bin/"
	}
    m.solution=sln_file
end

task :default => ["deps","build","tests"]
