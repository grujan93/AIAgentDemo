
using System.Collections.Concurrent;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SemanticKernelGeminiDemo.Chat;

public class ChatHistoryStore
{
    private readonly ConcurrentDictionary<string, ChatHistory> _histories = new();

    public ChatHistory GetOrCreate(string sessionId)
    {
        return _histories.GetOrAdd(sessionId, _ =>
        {
            var history = new ChatHistory();

            history.AddSystemMessage("""
            Ti si koristan AI asistent.
            Odgovaraj jednostavno, kratko i jasno.
            Korisnik je junior .NET developer.
            Za informacije i pitanja prvo proveri zvanicnu Microsoft dokumentaciju a ako ne nadjes onda koristi ostale izvore.

            Ako korisnik pita za vreme koristi dostupnu funkciju.
            Nemoj racunati matematiku iz glave ako postoji dostupna funkcija.
            Ako postoji task koji korisnik zeli da doda, vidi ili zavrsi task koristi dostupne todo funkcije.

            Nemoj izmisljati taskove. Ako treba listu taskova, koristi funkciju GetTasks.
        
            """);

            return history;
        });
    }
}