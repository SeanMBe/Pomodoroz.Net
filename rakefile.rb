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
	dlls = FileList["spec/unit/**/bin/**/*.spec.dll"]
	if dlls.count == 0 then 
		raise 'There should be at least one unit test. Check dlls test convention in this task'
	end
	dlls.each { |dll| sh "\"#{mspec}\" \"#{dll}\""}

end

desc "Run Acceptance Tests"
task :acceptance do
	puts 'acceptance tests'
end

desc "All tests"
task :tests => ["unit","acceptance"] do
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
