;==============================================================
; WLA-DX banking setup.
;==============================================================
.MEMORYMAP
DEFAULTSLOT 0
SLOTSIZE $7FF0
SLOT 0 $0000
SLOTSIZE $10
SLOT 1 $7FF0
SLOTSIZE $4000
SLOT 2 $8000
SLOTSIZE $4000
SLOT 3 $C000
SLOTSIZE $4000
SLOT 4 $10000
.ENDME

.ROMBANKMAP
BANKSTOTAL 4
BANKSIZE $7FF0
BANKS 1
BANKSIZE $10
BANKS 1
BANKSIZE $4000
BANKS 1
BANKSIZE $4000
BANKS 1
.ENDRO

.DEFINE PSGMOD_START_ADDRESS     $C100
.DEFINE PSGMOD_SUPPORT_GG_STEREO 0
.DEFINE PSGMOD_PSG_PORT          $7F

;==============================================================
; SDSC tag and SMS rom header
;==============================================================
.SDSCTAG 1.0,"3D City","3D City SMS Competition 2016","StevePro Studios"

.EMPTYFILL $00                ;ret.
.COMPUTESMSCHECKSUM           ;compute sms checksum for this rom.

.BANK 0 SLOT 0
.ORG $0000


.INCBIN "main.bin"

.ORG $0066
;==============================================================
; Pause button handler
;==============================================================
    ; Do nothing
    ;RETN

	
;==============================================================
; Include all of the music data. It must go in slot 2.
;==============================================================
.SLOT 2
.SECTION "Game Music" SUPERFREE
    .INCBIN "content/music.epsgmod"
.ends


.BANK 3 SLOT 2
.ORG $0000
PSGMOD_VIBRATO_TABLES: