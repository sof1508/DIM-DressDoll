﻿using System;
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
        Doll doll;

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

            if (!semantics.ContainsKey("partesArriba") && !semantics.ContainsKey("partesAbajo"))
            {
                synth.Speak("Lo siento no te entiendo");
                //synth.Speak("Si desea jugar prueba a decir");
            }
            else
            {
                synth.Speak("hola estoy funcionando");
                if (semantics.ContainsKey("Ponerse")) {
                
                    if (semantics.ContainsKey("partesAbajo")) {
                         
                        parteAbajo.Image = DressDoll.Properties.Resources.Summer_Skirt;
                    }
                          
                }

                if (semantics.ContainsKey("Quitarse")) { }
            }
            
        }
        

        private Grammar CreateGrammarBuilderDollDressSemantics(params int[] info)
        {
            //synth.Speak("Creando ahora la gramática");
            Choices partesAbajoChoice = new Choices();
            Choices partesArribaChoice = new Choices();
            Choices parteEnteraChoice = new Choices();
            Choices zapatosChoice = new Choices();

            // Partes de abajo 
            SemanticResultValue choiceResultValue =
                    new SemanticResultValue("Falda", "Summer_Skirt");
            GrammarBuilder resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesAbajoChoice.Add(resultValueBuilder);

            choiceResultValue =
                   new SemanticResultValue("Pantalón", "Autumn Pants");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesAbajoChoice.Add(resultValueBuilder);
            // Fin partes de abajo

            // Partes de arriba 
            new SemanticResultValue("Camiseta", "");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesArribaChoice.Add(resultValueBuilder);

            new SemanticResultValue("Chaqueta", "Spring Jacket");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesArribaChoice.Add(resultValueBuilder);

            new SemanticResultValue("Abrigo", "Autumn Coat");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesArribaChoice.Add(resultValueBuilder);
            // Fin partes de arriba

            // Partes entera
            new SemanticResultValue("Vestido", "");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            parteEnteraChoice.Add(resultValueBuilder);

            new SemanticResultValue("Festivo", "");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            parteEnteraChoice.Add(resultValueBuilder);
            // Fin partes enteras

            // Zapatos
            new SemanticResultValue("Zapatos", "");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            zapatosChoice.Add(resultValueBuilder);

            new SemanticResultValue("Botas", "");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            zapatosChoice.Add(resultValueBuilder);
            // Fin zapatos

            SemanticResultKey choiceResultKeyArriba = new SemanticResultKey("partesArriba", partesArribaChoice);
            SemanticResultKey choiceResultKeyAbajo = new SemanticResultKey("partesAbajo", partesAbajoChoice );
            GrammarBuilder partesArriba = new GrammarBuilder(choiceResultKeyArriba);
            GrammarBuilder partesAbajo = new GrammarBuilder(choiceResultKeyAbajo);
            

            //EN PROCESO
            GrammarBuilder poner = "Poner";
            GrammarBuilder cambiar = "Cambiar";
            GrammarBuilder quitar = "Quitar";
            GrammarBuilder ir = "Ir";

            SemanticResultKey ponerKey = new SemanticResultKey("Poner", poner);
            SemanticResultKey cambiarKey = new SemanticResultKey("Cambiar", cambiar);
            SemanticResultKey quitarKey = new SemanticResultKey("Quitar", quitar);
            SemanticResultKey irKey = new SemanticResultKey("Ir", ir);

            Choices alternativas = new Choices(poner, cambiar);
            //Choices alternativas = new Choices(poner, cambiar,quitar, ir);

            GrammarBuilder frase = new GrammarBuilder(alternativas);

            //HACER FRASE CON LA GRAMATICA ESTABLECIDA
            if (alternativas != null) {
                frase = new GrammarBuilder(alternativas);
            } else if (true) {

            }
            else { }
           

            // FIN DEL PROCESO

            if(partesArriba != null) { frase.Append(partesArriba);}

            if (partesAbajo != null) { frase.Append(partesAbajo); }
            

            Grammar grammar = new Grammar(frase);
            grammar.Name = "Poner/Cambiar";
            

            //Grammar grammar = new Grammar("so.xml.txt");

            return grammar;



        }

    }
}
