using SorteioTimes.Context;
using SorteioTimes.Domain;

using (var db = new SorteioTimesContext())
{
    ExibirMenu();
    List<Jogador> jogadores = db.Jogadores.ToList();
    bool sair = false;
    while (!sair)
    {
        Console.Write("Escolha uma opção: ");
        string opcao = Console.ReadLine();
        
        switch (opcao)
        {
            case "1":
                AdicionarJogador(db, jogadores);
                break;
            case "2":
                ExcluirJogador(db);
                break;
            case "3":
                GerarTimes(jogadores);
                break;
            case "4":
                ListarJogadores(db);
                break;
            case "5":
                AlterarNotaJogador(db);
                break;
            case "6":
                ExcluirTodosJogadores(db);
                jogadores.Clear();
                break;
            case "7":
                sair = true;
                break;
            default:
                Console.WriteLine("Opção inválida. Tente novamente.");
                break;
        }
        ExibirMenu();
        Console.WriteLine();
    }
}
static void ExibirMenu()
{
    Console.WriteLine("MENU");
    Console.WriteLine("1. Adicionar jogador");
    Console.WriteLine("2. Excluir jogador");
    Console.WriteLine("3. Gerar times");
    Console.WriteLine("4. Listar jogadores");
    Console.WriteLine("5. Alterar nota do jogador");
    Console.WriteLine("6. Excluir todos os jogadores");
    Console.WriteLine("7. Sair");
    Console.WriteLine();
}
static void AdicionarJogador(SorteioTimesContext db, List<Jogador> jogadores)
{
    Console.WriteLine("Digite o nome do jogador:");
    string nome = Console.ReadLine();

    Console.WriteLine("Digite a nota do jogador:");
    int nota = int.Parse(Console.ReadLine());

    db.Jogadores.Add(new Jogador { Nome = nome, Nota = nota });
    db.SaveChanges();

    Console.WriteLine("Jogador adicionado com sucesso!");
}
static void GerarTimes(List<Jogador> jogadores)
{
    Console.WriteLine("Digite o número de jogadores por time:");
    int jogadoresPorTime = int.Parse(Console.ReadLine());

    Console.WriteLine("Digite o número de times:");
    int numTimes = int.Parse(Console.ReadLine());

    if (jogadores.Count < jogadoresPorTime * numTimes)
    {
        Console.WriteLine("Não há jogadores suficientes para formar os times.");
        return;
    }

    Random random = new Random();
    List<Jogador> jogadoresEmbaralhados = jogadores.OrderBy(x => random.Next()).ToList();

    List<List<Jogador>> times = new List<List<Jogador>>();

    for (int i = 0; i < numTimes; i++)
    {
        times.Add(new List<Jogador>());
    }

    int index = 0;
    for (int i = 0; i < jogadoresPorTime; i++)
    {
        for (int j = 0; j < numTimes; j++)
        {
            Jogador jogador = jogadoresEmbaralhados[index];
            times[j].Add(jogador);
            index++;
        }
    }

    Console.WriteLine("Times:");

    for (int i = 0; i < numTimes; i++)
    {
        Console.WriteLine();
        Console.WriteLine($"Time {i + 1}:");

        double somaNotas = 0;
        foreach (Jogador jogador in times[i])
        {
            Console.WriteLine($"{jogador.Nome} - {jogador.Nota}");
            somaNotas += jogador.Nota;
        }

        double media = (double)somaNotas / jogadoresPorTime;
        Console.WriteLine($"Média: {media:F2}");
        Console.WriteLine();
    }
}
static void ListarJogadores(SorteioTimesContext db)
{
    var jogadores = db.Jogadores.OrderBy(j => j.Nome).ToList();

    Console.WriteLine("Lista de Jogadores:");

    foreach (var jogador in jogadores)
    {
        Console.WriteLine($"{jogador.Id} - {jogador.Nome}, Nota: {jogador.Nota}");
    }
}
static void AlterarNotaJogador(SorteioTimesContext db)
{
    ListarJogadores(db);
    Console.WriteLine("Digite o ID do jogador que deseja alterar a nota:");
    int id = int.Parse(Console.ReadLine());

    Jogador jogador = db.Jogadores.Find(id);
    if (jogador != null)
    {
        Console.WriteLine($"Jogador encontrado: {jogador.Nome} - Nota atual: {jogador.Nota}");

        Console.WriteLine("Digite a nova nota do jogador:");
        int novaNota = int.Parse(Console.ReadLine());

        jogador.Nota = novaNota;
        db.SaveChanges();

        Console.WriteLine("Nota do jogador alterada com sucesso!");
    }
    else
    {
        Console.WriteLine("Jogador não encontrado!");
    }
}
static void ExcluirJogador(SorteioTimesContext db)
{
    Console.WriteLine("Digite o ID do jogador que deseja excluir:");
    int id = int.Parse(Console.ReadLine());

    Jogador jogador = db.Jogadores.Find(id);
    if (jogador != null)
    {
        db.Jogadores.Remove(jogador);
        db.SaveChanges();
        Console.WriteLine("Jogador excluído com sucesso!");
    }
    else
    {
        Console.WriteLine("Jogador não encontrado!");
    }
}
static void ExcluirTodosJogadores(SorteioTimesContext db)
{
    var jogadores = db.Jogadores.ToList();
    db.Jogadores.RemoveRange(jogadores);
    db.SaveChanges();

    Console.WriteLine("Todos os jogadores foram excluídos.");
}
