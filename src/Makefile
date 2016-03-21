OUTPUT=3dcity.sms
TARGET=main
OBJECT=*tile.o *manager.o *screen.o

all: $(TARGET)

$(TARGET): $(OBJECT)
	zcc +sms $(OBJECT) main.c -o main.bin -m -create-app
	wla-z80 -o main.s
	wlalink -v linkfile 3dcity.sms
	rm -f *.bin *.map zcc*
	cp $(OUTPUT) ..
	cp $(OUTPUT) ..\asm

%.o: %.c
	zcc +sms -vn -make-lib $< $^

clean:
	rm -f *.bin *.map *.sms zcc*