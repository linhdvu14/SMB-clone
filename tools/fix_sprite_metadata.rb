filename = './Assets/Sprites/4096x3200.png.meta'
buffer = []
File.open(filename, 'r') do |f|
  f.each_line do|l|
    if l.include?('        x:') || l.include?('        y:') || l.include?('        width:') || l.include?('        height:')
      n = l.split(' ').last.to_i
      puts "original: #{n}, edited: #{n*8}"
      buffer.append(l.gsub(n.to_s, (n*8).to_s))
    elsif l.include?('      outline:')
      buffer.append(l.chomp + " []\n")
    elsif l.include?('        - ') || l.include?('      - - ')
      # do not append
    else
      buffer.append(l)
    end
  end
  puts buffer
end

File.open(filename, 'w') do |f|
  f.puts(buffer)
end
