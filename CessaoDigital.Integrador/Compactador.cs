// Copyright (c) 2023 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (API's) da Plataforma Cessão Digital.

using System.IO.Compression;

namespace CessaoDigital.Integrador
{
    public static class Compactador
    {
        public static byte[] Compactar(IEnumerable<(string nome, byte[] conteudo)> documentos)
        {
            using (var msZip = new MemoryStream())
            {
                using (var zip = new ZipArchive(msZip, ZipArchiveMode.Create, true))
                    foreach (var item in documentos)
                        using (var entry = zip.CreateEntry(item.nome, CompressionLevel.Fastest).Open())
                            entry.Write(item.conteudo, 0, item.conteudo.Length);

                return msZip.ToArray();
            }
        }

        public static IEnumerable<(string nome, byte[] conteudo)> Descompactar(byte[] conteudoCompactado)
        {
            using (var msZip = new MemoryStream(conteudoCompactado))
            {
                using (var zip = new ZipArchive(msZip, ZipArchiveMode.Read))
                {
                    foreach (var item in zip.Entries)
                    {
                        using (var ms = new MemoryStream())
                        {
                            item.Open().CopyTo(ms);

                            yield return (item.FullName, ms.ToArray());
                        }
                    }
                }
            }
        }
    }
}