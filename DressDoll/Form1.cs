using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace DressDoll
{
    public partial class Form1 : Form
    {
     private System.Speech.Recognition.SpeechRecognitionEngine _recognizer =
        new SpeechRecognitionEngine();
        private SpeechSynthesizer synth = new SpeechSynthesizer();

        /* VARIABLES GLOBALES */
        Doll doll = new Doll(false,false,false,false);
        bool ChicaOn = true;
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        public Form1()
        {
            InitializeComponent();
            InicializarImagenes();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            synth.Speak("Inicializando la Aplicación");

            Grammar grammar = CreateGrammarBuilderDollDressSemantics(null);
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.UnloadAllGrammars();
            // Nivel de confianza del reconocimiento 70%
            _recognizer.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", 50);
            grammar.Enabled = true;
            _recognizer.LoadGrammar(grammar);
            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_recognizer_SpeechRecognized);
            //reconocimiento asíncrono y múltiples veces
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            synth.Speak("Aplicación preparada para reconocer su voz");
        }


        void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //obtenemos un diccionario con los elementos semánticos
            SemanticValue semantics = e.Result.Semantics;

            string rawText = e.Result.Text;
            RecognitionResult result = e.Result;

            if (!semantics.ContainsKey("Lugares"))
            {
                if (!semantics.ContainsKey("Poner"))
                {
                    if (!semantics.ContainsKey("Apagar"))
                    {
                        if (!semantics.ContainsKey("Cambiar"))
                        {
                            if (!semantics.ContainsKey("Encender"))
                            {
                            }
                            else 
                            {
                                String musica = ((string)semantics["musica"].Value);
                                synth.Speak("Poniendo " + musica);
                                switch (musica) 
                                {
                                    case "musica":
                                        player.SoundLocation = "../../Resources/musica.wav";
                                        player.Load();
                                        player.Play();
                                        break;
                                    /*case "villancico":
                                        player.SoundLocation = "../../Resources/Jingle Bells.wav";
                                        player.Load();
                                        player.Play();
                                        break;*/
                                        
                                }
                                    
                            }
                        }
                        else
                        {
                            String sexo = ((string)semantics["Sexo"].Value);
                            synth.Speak("Cambiando " + sexo);
                            switch (sexo)
                            {
                                case "Chico":
                                    cuerpo.Image = DressDoll.Properties.Resources.Boy;
                                    ChicaOn = false;
                                    break;
                                case "Chica":
                                    cuerpo.Image = DressDoll.Properties.Resources.Girl_1;
                                    ChicaOn = true;
                                    break;
                                case "Sexo":
                                    if (ChicaOn)
                                    {
                                        cuerpo.Image = DressDoll.Properties.Resources.Boy;
                                        ChicaOn = false;
                                    }
                                    else
                                    {
                                        cuerpo.Image = DressDoll.Properties.Resources.Girl_1;
                                        ChicaOn = true;
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (semantics.ContainsKey("musica"))
                        {
                            synth.Speak("Apagando música");
                            player.Stop();
                        } 
                    }
                }
                else {
                    if (semantics.ContainsKey("partesAbajo"))
                    {
                        String ropa = ((string)semantics["partesAbajo"].Value);
                        synth.Speak("Poniendo " + ropa);
                        if (doll.conflictoRopa("ParteAbajo"))
                        {
                            //quitar parte parteEntera.Image
                            parteEntera.Image = null;
                            //poner a false en doll
                            doll.ParteEntera = false;
                        }
                        switch (ropa)
                        {
                            case "Falda":
                                parteAbajo.Image = DressDoll.Properties.Resources.Falda;
                                break;
                            case "Pantalones":
                                parteAbajo.Image = DressDoll.Properties.Resources.Pantalones;
                                break;
                            case "Pantalones cortos":
                                parteAbajo.Image = DressDoll.Properties.Resources.Shorts;
                                break;
                        }
                        //poner a true  parte Abajo doll
                        doll.ParteAbajo = true;
                    }
                    else if (semantics.ContainsKey("partesArriba"))
                    {
                        String ropa = ((string)semantics["partesArriba"].Value);
                        synth.Speak("Poniendo " + ropa);
                        if (doll.conflictoRopa("ParteArriba"))
                        {
                            //quitar parte parteEntera.Image
                            parteEntera.Image = null;
                            //poner a false en doll
                        }
                        switch (ropa)
                        {
                            case "Camiseta":
                                parteArriba.Image = DressDoll.Properties.Resources.Camiseta;
                                break;
                            case "Chaqueta":
                                parteArriba.Image = DressDoll.Properties.Resources.Chaqueta;
                                break;
                            case "Abrigo":
                                parteArriba.Image = DressDoll.Properties.Resources.Abrigo;
                                break;
                            case "Blusa":
                                parteArriba.Image = DressDoll.Properties.Resources.Blusa;
                                break;
                            case "Jersey":
                                parteArriba.Image = DressDoll.Properties.Resources.Jersey;
                                break; 
                        }
                        //poner a true  parte Arriba doll
                        doll.ParteArriba = true;
                    }
                    else if (semantics.ContainsKey("zapatos"))
                    {
                        String ropa = ((string)semantics["zapatos"].Value);
                        synth.Speak("Poniendo " + ropa);
                        switch (ropa)
                        {
                            case "Zapatos":
                                zapatos.Image = DressDoll.Properties.Resources.Zapatos;
                                break;
                            case "Botas":
                                zapatos.Image = DressDoll.Properties.Resources.Botas;
                                break;
                            case "Sandalias":
                                zapatos.Image = DressDoll.Properties.Resources.Sandalias;
                                break;
                    
                        }
                        // poner a true  zapatos doll
                        doll.Zapatos = true;
                    }
                    else if (semantics.ContainsKey("parteEntera"))
                    {
                        String ropa = ((string)semantics["parteEntera"].Value);
                        synth.Speak("Poniendo " + ropa);
                        if (doll.conflictoRopa("ParteEntera"))
                        {
                            //quitar parte  Arriba y abajo parteEntera.Image
                            parteArriba.Image = null;
                            parteAbajo.Image = null;
                            //poner a false las dos en doll
                            doll.ParteArriba = false;
                            doll.ParteAbajo = false;
                        }
                        switch (ropa)
                        {
                            case "Vestido":
                                parteEntera.Image = DressDoll.Properties.Resources.Vestido;
                                break;
                            case "Playero":
                                parteEntera.Image = DressDoll.Properties.Resources.Playero;
                                break;
                            case "Bikini":
                                parteEntera.Image = DressDoll.Properties.Resources.Bikini;
                                break;
                            case "Bañador":
                                parteEntera.Image = DressDoll.Properties.Resources.Bañador;
                                break;
                            case "Disfraz navideño":
                                parteEntera.Image = DressDoll.Properties.Resources.TrajeFestivo;
                                this.BackgroundImage = DressDoll.Properties.Resources.navidad;
                                //musica
                                player.SoundLocation = "../../Resources/Jingle Bells.wav";
                                player.Load();
                                player.Play();
                                break;
                            case "Disfraz de pirata":
                                parteEntera.Image = DressDoll.Properties.Resources.disfraz;
                                this.BackgroundImage = DressDoll.Properties.Resources.barco;
                                //musica
                                player.SoundLocation = "../../Resources/pirata.wav";
                                player.Load();
                                player.Play();
                                break;
                        }
                        // poner a true  parte Entera doll
                        doll.ParteEntera = true;
                    }
                }
                
                if (semantics.ContainsKey("Quitar"))
                {

                    if (semantics.ContainsKey("partesArriba"))
                    {
                        synth.Speak("Quitando " + semantics["partesArriba"].Value);
                        parteArriba.Image = null;
                    }
                    else if (semantics.ContainsKey("partesAbajo"))
                    {
                        synth.Speak("Quitando " + semantics["partesAbajo"].Value);
                        parteAbajo.Image = null;
                    }
                    else if (semantics.ContainsKey("zapatos"))
                    {
                        synth.Speak("Quitando " + semantics["zapatos"].Value);
                        zapatos.Image = null;
                    }
                    else if (semantics.ContainsKey("parteEntera"))
                    {
                        synth.Speak("Quitando " + semantics["parteEntera"].Value);
                        parteEntera.Image = null;
                    }
                }
            }
            else
            {
                
                String lugar = ((string)semantics["Lugares"].Value);
                synth.Speak("Yendo a " + lugar);
                switch (lugar)
                {
                    case "playa":
                        this.BackgroundImage = DressDoll.Properties.Resources.playa;
                        break;
                    case "parque":
                        this.BackgroundImage = DressDoll.Properties.Resources.parque;
                        break;
                    case "barco":
                        this.BackgroundImage = DressDoll.Properties.Resources.barco;
                        break;
                    case "casa":
                        this.BackgroundImage = DressDoll.Properties.Resources.navidad;
                        break;
                    case "cine":
                        this.BackgroundImage = DressDoll.Properties.Resources.cine;
                        break;
                    case "clase":
                        this.BackgroundImage = DressDoll.Properties.Resources.clase;
                        break;

                }
            }
        }
        
        private Grammar CreateGrammarBuilderDollDressSemantics(params int[] info)
        {
            //synth.Speak("Creando ahora la gramática");
            Choices partesAbajoChoice = new Choices();
            Choices partesArribaChoice = new Choices();
            Choices parteEnteraChoice = new Choices();
            Choices zapatosChoice = new Choices();
            Choices musicaChoice = new Choices();

            // Partes de abajo 
            SemanticResultValue choiceResultValue =
                    new SemanticResultValue("Falda", "Falda");
            GrammarBuilder resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesAbajoChoice.Add(resultValueBuilder);

            choiceResultValue =
                   new SemanticResultValue("Pantalones", "Pantalones");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesAbajoChoice.Add(resultValueBuilder);

            choiceResultValue =
                   new SemanticResultValue("Pantalones cortos", "Pantalones cortos");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesAbajoChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("parte de abajo", "Parte de abajo");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesAbajoChoice.Add(resultValueBuilder);
            // Fin partes de abajo

            // Partes de arriba 
            choiceResultValue = new SemanticResultValue("Camiseta", "Ca" +
                "miseta");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesArribaChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Chaqueta", "Chaqueta");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesArribaChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Abrigo", "Abrigo");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesArribaChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Blusa", "Blusa");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesArribaChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Jersey", "Jersey");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesArribaChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("parte de arriba", "Parte de arriba");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesArribaChoice.Add(resultValueBuilder);
            // Fin partes de arriba

            // Partes entera
            choiceResultValue = new SemanticResultValue("Vestido", "Vestido");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            parteEnteraChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Vestido playero", "Playero");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            parteEnteraChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Disfraz festivo", "Disfraz navideño");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            parteEnteraChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Disfraz de pirata", "Disfraz de pirata");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            parteEnteraChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Bañador", "Bañador");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            parteEnteraChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Bikini", "Bikini");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            parteEnteraChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("atuendo", "Atuendo");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            parteEnteraChoice.Add(resultValueBuilder);
            // Fin partes enteras

            // Zapatos
            choiceResultValue = new SemanticResultValue("Zapatos", "Zapatos");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            zapatosChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Botas", "Botas");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            zapatosChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Sandalias", "Sandalias");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            zapatosChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("calzado", "Calzado");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            zapatosChoice.Add(resultValueBuilder);
            // Fin zapatos

            // Música
            choiceResultValue = new SemanticResultValue("Música", "musica");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            musicaChoice.Add(resultValueBuilder);

           /* choiceResultValue = new SemanticResultValue("Villancico", "villancico");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            musicaChoice.Add(resultValueBuilder);*/


            // Fin música

            //Creación de palabras claves 
            SemanticResultKey choiceResultKeyArriba = new SemanticResultKey("partesArriba", partesArribaChoice);
            SemanticResultKey choiceResultKeyAbajo = new SemanticResultKey("partesAbajo", partesAbajoChoice);
            SemanticResultKey choiceResultKeyZapatos = new SemanticResultKey("zapatos", zapatosChoice);
            SemanticResultKey choiceResultKeyEntero = new SemanticResultKey("parteEntera", parteEnteraChoice);
            SemanticResultKey choiceResultKeyMusica = new SemanticResultKey("musica", musicaChoice);

            //Creación de grammarbuilder 
            GrammarBuilder partesArriba = new GrammarBuilder(choiceResultKeyArriba);
            GrammarBuilder partesAbajo = new GrammarBuilder(choiceResultKeyAbajo);
            GrammarBuilder zapatos = new GrammarBuilder(choiceResultKeyZapatos);
            GrammarBuilder partesEnteras = new GrammarBuilder(choiceResultKeyEntero);
            GrammarBuilder musica = new GrammarBuilder(choiceResultKeyMusica);

            Choices opcionesRopa = new Choices(new GrammarBuilder[] { partesArriba, partesAbajo, zapatos, partesEnteras});

            //Opciones de lugares para ir
            Choices lugaresChoice = new Choices();
            choiceResultValue =
                   new SemanticResultValue("Pasear", "parque");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            lugaresChoice.Add(resultValueBuilder);

            choiceResultValue =
                   new SemanticResultValue("Playa", "playa");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            lugaresChoice.Add(resultValueBuilder);

            choiceResultValue =
                   new SemanticResultValue("Barco", "barco");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            lugaresChoice.Add(resultValueBuilder);

            choiceResultValue =
                   new SemanticResultValue("Casa por navidad", "casa");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            lugaresChoice.Add(resultValueBuilder);

            choiceResultValue =
                   new SemanticResultValue("Cine", "cine");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            lugaresChoice.Add(resultValueBuilder);

            choiceResultValue =
                  new SemanticResultValue("Clase", "clase");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            lugaresChoice.Add(resultValueBuilder);

            SemanticResultKey choiceResultKeyLugares = new SemanticResultKey("Lugares", lugaresChoice);

            GrammarBuilder lugares = new GrammarBuilder(choiceResultKeyLugares);


            //Opciones de sexo
            Choices sexoChoice = new Choices();
            choiceResultValue =
                   new SemanticResultValue("Chico", "Chico");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            sexoChoice.Add(resultValueBuilder);

            choiceResultValue =
                   new SemanticResultValue("Chica", "Chica");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            sexoChoice.Add(resultValueBuilder);

            choiceResultValue =
                   new SemanticResultValue("Sexo", "Sexo");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            sexoChoice.Add(resultValueBuilder);

            SemanticResultKey choiceResultKeySexo = new SemanticResultKey("Sexo", sexoChoice);

            GrammarBuilder sexo = new GrammarBuilder(choiceResultKeySexo);

            //FRASES
            GrammarBuilder poner = "Poner";
            GrammarBuilder cambiar = "Cambiar";

            Choices ponerAlternativa = new Choices(poner, cambiar);   
            SemanticResultKey choiceResultKeyPoner = new SemanticResultKey("Poner", ponerAlternativa);
            GrammarBuilder ponerFrase = new GrammarBuilder(choiceResultKeyPoner);
            ponerFrase.Append(opcionesRopa);

            GrammarBuilder quitar = "Quitar";
            GrammarBuilder quitarFrase = new GrammarBuilder(new SemanticResultKey("Quitar", new Choices(quitar)));
            quitarFrase.Append(opcionesRopa);

            GrammarBuilder apagar = "Apagar";
            Choices apagarAlternativa = new Choices(quitar, apagar);
            SemanticResultKey choiceResultKeyQuitar = new SemanticResultKey("Apagar", apagarAlternativa);
            GrammarBuilder apagarFrase = new GrammarBuilder(choiceResultKeyQuitar);
            apagarFrase.Append(musica);

            GrammarBuilder encender = "Encender";
            Choices encenderAlternativa = new Choices(poner, encender);
            SemanticResultKey choiceResultKeyEncender = new SemanticResultKey("Encender", encenderAlternativa);
            GrammarBuilder encenderFrase = new GrammarBuilder(choiceResultKeyEncender);
            encenderFrase.Append(musica);

            GrammarBuilder cambiarA = "Cambiar a";
            GrammarBuilder cambiarDe = "Cambiar de";
            Choices cambiarDeAlternativa = new Choices(cambiarA, cambiarDe);
            SemanticResultKey choiceResultKeyCambiar = new SemanticResultKey("Cambiar", cambiarDeAlternativa);
            GrammarBuilder cambiarFrase = new GrammarBuilder(choiceResultKeyCambiar);
            cambiarFrase.Append(sexo);


            GrammarBuilder fondoFrase = new GrammarBuilder("Ir a");
            fondoFrase.Append(lugares);

            Choices opcionesFrase = new Choices(new GrammarBuilder[] {ponerFrase, quitarFrase, apagarFrase, encenderFrase, fondoFrase, cambiarFrase});
            Grammar grammar = new Grammar((GrammarBuilder)opcionesFrase);
            grammar.Name = "Poner/Cambiar";
            return grammar;

            //Grammar grammar = new Grammar("so.xml.txt");
        }

    }
}
