
using System; 
using System.Net.NetworkInformation;
using System.Net;
using System.Linq;
namespace Ping_scan  
{  
    class scanner {  
          
        static void Main(string[] args) 
        {  
            if(args.Length ==1)
            {
                
                Ping aPing = new Ping();
                PingReply reply = aPing.Send(args[0], 1000);
                if(reply.Status==IPStatus.Success){
                    Console.WriteLine($"response from: {args[0]}");
                }
            }
            else if(args.Length ==2){

                IPaddress StartIP= new IPaddress(args[0]);
                IPaddress EndIP= new IPaddress(args[1]);
                EndIP.increment();
                while(StartIP<EndIP)
                {
                    Ping aPing = new Ping();
                    PingReply reply = aPing.Send(StartIP.ToString(), 1000);
                    if(reply.Status==IPStatus.Success){
                        Console.WriteLine($"response from: {StartIP}");
                        }
                    StartIP.increment();
                }
            }          
            else
            {
                Console.WriteLine("Usage: ping_scan [ first ip address] [last IP address]");
            }
    }   }
}

// We're not going to check to make sure the value is a valid IP address.
public class IPaddress{
    public int[] intOctet= new int[4];
// constuctor to split the string by .'s and store the integrer version of stuff into array
    public IPaddress(string anAddress){
        string[] octets = anAddress.Split('.');
        bool res;
        for(int i=0;i <4;i++){
            res=int.TryParse(octets[i], out intOctet[i]);
            
            if (!res)
            {
                throw new Exception("Not an integer");
            }
        }
        

    }
    // increment function it tries to increment each octet unless its value is 255
    // in that case the next octet is incremented and the previous octets are set to zero 
    public void increment(){
        if(intOctet[3]!=255){
            intOctet[3]++;
        }
        else if(intOctet[2]!= 255){
            intOctet[2]++;
            intOctet[3]=0;
        }
        else if(intOctet[1]!= 255){
            intOctet[1]++;
            intOctet[3]=0;
            intOctet[2]=0;
        }
        else if(intOctet[0]!= 255){
            intOctet[0]++;
            intOctet[3]=0;
            intOctet[2]=0;
            intOctet[1]=0;
        }
    }
    // just returns Ip in dotted decimal notation 
    public override string ToString(){
        return intOctet[0].ToString()+"."+ intOctet[1].ToString()+"."+intOctet[2].ToString()+"."+intOctet[3].ToString();
        // return Str(intOctet[0])+Str(intOctet[1])+Str(intOctet[2])+Str(intOctet[3]);
    }

    public bool Equals(IPaddress anIP){
        if(anIP.ToString()==this.ToString()){
            return true;
        }
        else{
            return false;
        }

    }
// Overload the < and > operator
     public static bool operator < (IPaddress Ip1,IPaddress Ip2){
         if(Ip1.intOctet[0]<Ip2.intOctet[0]){
             return true;
         }
         else if (Ip1.intOctet[1]<Ip2.intOctet[1]){
             return true;
         }
         else if (Ip1.intOctet[2]<Ip2.intOctet[2]){
             return true;
         }
         else if (Ip1.intOctet[3]<Ip2.intOctet[3]){
             return true;
         }
         else{
            //  Console.WriteLine(Ip1);
            //  Console.WriteLine(Ip2);
             return false;
         }
     }

     public static bool operator > (IPaddress Ip1,IPaddress Ip2){
         if(Ip1.intOctet[0]<=Ip2.intOctet[0]){
             return false;
         }
         else if (Ip1.intOctet[1]<=Ip2.intOctet[1]){
             return false;
         }
         else if (Ip1.intOctet[2]<=Ip2.intOctet[2]){
             return false;
         }
         else if (Ip1.intOctet[3]<=Ip2.intOctet[3]){
             return false;
         }
         else{
             return true;
         }
     }


}
