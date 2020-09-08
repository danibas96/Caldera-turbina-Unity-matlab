clc,clear all,close all;
%% VARIABLES DE LOS ESTADOS DEL SISTEMA
T(1)=0;
Pr(1)=0;
P(1)=0;
%% TIEMPO DE SIMULACION
ti=0;
tfinal=120;
ts1=0.1;
t=ti:ts1:tfinal;
%% SENALES DE ENTRADA DEL SISTEMA
Va(1)=0;
Vf(1)=0;
Vv(1)=0;
%% COMUNICACION MEMORIAS
loadlibrary('smClient64.dll','./smClient.h');
%% ABRIR MEMORIA COMPARTIDA
calllib('smClient64','openMemory','Sistema',2);
%% GRAFICAS EN TIEMPO REAL
figure
axis equal;
h3=plot(t(1),T(1),'--r'); hold on
h4=plot(t(1),Pr(1),'--g'); grid on
h5=plot(t(1),P(1),'--b'); grid on

h6=plot(t(1),Va(1),'--c'); hold on
h7=plot(t(1),Vf(1),'--y'); grid on
h8=plot(t(1),Vv(1),'--m'); grid on

title('Animacion')
xlabel('tiempo [s]'); ylabel('estados');
for k=1:length(t)
   tic;
   drawnow;
   delete(h3);
   delete(h4);
   delete(h5);
   delete(h6);
   delete(h7);
   delete(h8);
   %% estado 1
   T(k)=14;
   %% estado 2
   Pr(k)=2;
   %% estado 3
   P(k)=10;
   %% ESCRITURA DE DE LOS ESTADOS DEL SISTEMA MC
   %calllib('smClient64','setFloat','Sistema',0,T(k));
   while true 
      calllib('smClient64','setFloat','Sistema',0,T(k));
      calllib('smClient64','setFloat','Sistema',1,Pr(k));
      calllib('smClient64','setFloat','Sistema',2,P(k));
      T(k)=T(k)+0.1/3000;
      Pr(k)=Pr(k)+0.1/4000;
      P(k)=P(k)+0.1/3000;
   end
   
   %% LECTURA DE SENAL DE ENTRADAS DEL SISTEMA MC
   Va(k+1) = calllib('smClient64','getFloat','Sistema',3);
   Vf(k+1) = calllib('smClient64','getFloat','Sistema',4);
   Vv(k+1) = calllib('smClient64','getFloat','Sistema',5);
   %% GRAFICAS DE ESTADOS DEL SISTEMA
   h3=plot(t(1:k),T(1:k),"--r");hold on 
   h4=plot(t(1:k),Pr(1:k),"--g");hold on
   h5=plot(t(1:k),P(1:k),"--b");hold on
   %% GRAFICAS DE ENTRAD ADEL SISTEMA
   h6=plot(t(1:k),T(1:k),"--c");hold on 
   h7=plot(t(1:k),Pr(1:k),"--y");hold on
   h8=plot(t(1:k),P(1:k),"--m");hold on
   legend('T','Pr','P','Va','Vf','Vv');
   grid on
   while(toc<ts1)
   end
   toc
end
%% LIBERAR MEMORIA COMPARTIDA
calllib('smClient64','freeViews')
unloadlibrary smClient64
%% grafica del sistema
figure()
subplot(3,1,1)
plot(t,T,'--r')
grid on;
hold on;
legend('Temperatura')
subplot(3,1,2)
plot(t,P,'--b')
hold on;
grid on;
legend('Potencia')
subplot(3,1,3)
plot(t,Pr,'--g')
hold on;
grid on;
legend('Presion')

