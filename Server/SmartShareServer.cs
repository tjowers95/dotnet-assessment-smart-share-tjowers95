using System.Net.Sockets;
using System.Net;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Core.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlSerializer fromClientDeserialize = new XmlSerializer(typeof(ClientToServerDTO));

            ClientToServerDTO fromClientDTO = new ClientToServerDTO();

            IPEndPoint ingress = new IPEndPoint(0, 1234);

            SmartShareContext DBContext = new SmartShareContext();

            TcpListener mainSocket = new TcpListener(ingress);
            mainSocket.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            mainSocket.Start();

            while (true)
            {
                try
                {
                    TcpClient clientConnection = mainSocket.AcceptTcpClient();
                    Console.WriteLine("Connection Received");

                    Task.Run(() =>
                    {
                        SmartShareContext dbContext = new SmartShareContext();
                        DbSet<SmartShareFile> smartShareFileTable = dbContext.SmartShareFileTable;
                        SmartShareFile newDataEntry = new SmartShareFile();

                        using (NetworkStream stream = clientConnection.GetStream())
                        {
                            fromClientDTO = (ClientToServerDTO)fromClientDeserialize.Deserialize(stream);

                            bool exists = (from i in smartShareFileTable where i.Filename == fromClientDTO.Filename select i).Any();

                            if (fromClientDTO.UserAction == UserAction.upload)
                            {
                                if (!exists)
                                {
                                    newDataEntry.Filename = fromClientDTO.Filename;
                                    newDataEntry.Password = fromClientDTO.Password;
                                    newDataEntry.MaximumDownloads = fromClientDTO.MaxDown;
                                    newDataEntry.Expiration = fromClientDTO.Expiration;
                                    newDataEntry.DownloadCount = 0;
                                    newDataEntry.FileData = fromClientDTO.FileData;

                                    dbContext.Add(newDataEntry);
                                    dbContext.SaveChanges();
                                }
                            }

                            if (fromClientDTO.UserAction == UserAction.download || fromClientDTO.UserAction == UserAction.view)
                            {
                                XmlSerializer toClientSerialize = new XmlSerializer(typeof(ServerToClientDTO));
                                ServerToClientDTO toClientDTO = new ServerToClientDTO();


                                if (exists)
                                {
                                    SmartShareFile fetchedDownload = (from i in smartShareFileTable
                                                                      where i.Filename == fromClientDTO.Filename
                                                                      select i).First();

                                    if (fetchedDownload.Password == fromClientDTO.Password && DateTime.Parse(fetchedDownload.Expiration) > DateTime.Now && fetchedDownload.DownloadCount < fetchedDownload.MaximumDownloads)
                                    {
                                        toClientDTO.Filename = fetchedDownload.Filename;
                                        toClientDTO.MaxDown = fetchedDownload.MaximumDownloads;
                                        toClientDTO.DownCount = ++fetchedDownload.DownloadCount;
                                        toClientDTO.Expiration = fetchedDownload.Expiration;
                                        toClientDTO.FileData = fetchedDownload.FileData;
                                        toClientDTO.Created = fetchedDownload.Created;

                                        if (fromClientDTO.UserAction != UserAction.view)
                                        {
                                            dbContext.Update(fetchedDownload);
                                            dbContext.SaveChanges();
                                        }

                                        --toClientDTO.DownCount;

                                        toClientSerialize.Serialize(stream, toClientDTO);
                                    }
                                    else if (DateTime.Parse(fetchedDownload.Expiration) <= DateTime.Now || fetchedDownload.MaximumDownloads <= fetchedDownload.DownloadCount)
                                    {
                                        dbContext.SmartShareFileTable.Remove(fetchedDownload);
                                        dbContext.SaveChanges();
                                    }
                                }
                            }
                        }

                        dbContext.Dispose();
                    });                 
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }


        }
    }
}