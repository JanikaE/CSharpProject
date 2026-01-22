import os
import sys

def convert_line_endings(root_dir, target_eol='\n'):
    for dirpath, dirnames, filenames in os.walk(root_dir):
        for filename in filenames:
            if filename.endswith(('.py', '.js', '.java', '.cpp', '.h', 
                                  '.html', '.css', '.json', '.txt', 'cs')):
                filepath = os.path.join(dirpath, filename)
                
                try:
                    with open(filepath, 'rb') as f:
                        content = f.read()
                    
                    content = content.replace(b'\r\n', b'\n')
                    content = content.replace(b'\r', b'\n')
                    if target_eol == '\r\n':
                        content = content.replace(b'\n', b'\r\n')
                    
                    with open(filepath, 'wb') as f:
                        f.write(content)
                        
                    print(f"1: {filepath}")
                    
                except Exception as e:
                    print(f"skip {filepath}: {e}")

if __name__ == "__main__":
    # sample£ºpython convert_eol.py /path/to/project LF
    root_dir = sys.argv[1] if len(sys.argv) > 1 else '.'
    target_eol = '\n' if len(sys.argv) <= 2 or sys.argv[2].upper() == 'LF' else '\r\n'
    
    convert_line_endings(root_dir, target_eol)