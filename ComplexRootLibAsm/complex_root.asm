.data	
	
	rroot dq 0h					;modulus real root to be calculated
	rrootinit dq 0h
	n dd 0h						;root
	
	mrootr dq 0h				;previous modulus real root result
	delta	dq 0h				;modulus real root result
	counter dd 0h				

	
.code
calculateRootsAsm PROC
	
	mov rax, 4022000000000000h
	mov [rroot], rax
	mov	[rrootinit], rax
	mov [delta], rax
	mov eax, 2h
	mov [n], eax
	fild [n]					;fpu stack: [n]
	fld st						;fpu stack: [n], [n]
	fld1						;fpu stack: 1.0, [n], [n]
	fxch st(1)					;fpu stack: [n], 1.0, [n]
	

	nth_root_loop:
		fsub st, st(1)			;fpu stack: [n]-1, 1.0, [n]
		fld [rroot]				;fpu stack: [rroot], [n]-1, 1.0, [n]
		fxch st(1);				;fpu stack: [n]-1, [rroot], 1.0, [n]
		fld [rroot]				;fpu stack: [rroot], [n]-1, [rroot], 1.0, [n]
		
		;calculate the (n-1)th power of [rroot] according to: [rroot] ^ ([n]-1) = 2 ^ (([n]-1) * log_2([rroot]))
		fyl2x					;st(0) = st(1) * log_2(st(0))					
		fld1					;load 1 to top
		fld st(1)				;copy logarithm result to top
		fprem					;st(0) = st(0) - (st(0) div st(1)) * st(1)
		f2xm1					;st(0) = 2 ^ (st(0)) - 1
		fadd					;st(0) += 1
		fscale					;scale st(0) by st(1)
		fxch st(1)				;move temp result top
		fstp st					;pop temp result, fpu stack: [rroot] ^ ([n]-1), [rroot], 1.0, [n]
		

		fld [rrootinit]			;fpu stack: [rrootinit], [rroot] ^ ([n]-1), [rroot], 1.0, [n]
		fdiv st, st(1)
		fsub st, st(2)
		fdiv st, st(4)
		fxch st(2)
		fadd st, st(2)
		fxch st(2)				;fpu stack: [delta], [rroot] ^ ([n]-1), [rroot] + [delta], 1.0, [n]
		
		fabs
		fstp [delta]			;[rroot] ^ ([n]-1), [rroot] + [delta], 1.0, [n]
		fstp st
		fstp [mrootr]
		fld st(1)				;fpu stack: [n], 1.0, [n]
						
						
	
	mov rax, [delta]
	cmp rax, 0000000000000000h
	jng end_nth_root_loop
	mov rax, [mrootr]
	sub rax, [rroot]
	jz end_nth_root_loop
	mov rax, [mrootr]
	mov [rroot], rax
	jmp nth_root_loop

	end_nth_root_loop:
		movq xmm0, [mrootr]
		
	ret
calculateRootsAsm ENDP



end