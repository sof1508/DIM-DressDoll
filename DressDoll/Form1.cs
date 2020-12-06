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
                if (!semantics.ContainsKey("Cambiar"))
                {
                    // if quitar
                }
                else {
                    synth.Speak("entrando a cambiar");
                    if (semantics.ContainsKey("partesAbajo"))
                    {
                        String ropa = ((string)semantics["partesAbajo"].Value);
                        if (doll.conflictoRopa("ParteAbajo"))
                        {
                            //quitar parte parteEntera.Image
                            //poner a false en doll
                        }
                        switch (ropa)
                        {
                            case "Summer_Skirt":
                                parteAbajo.Image = DressDoll.Properties.Resources.Summer_Skirt;
                                break;
                            case "Autumn_Pants":
                                parteAbajo.Image = DressDoll.Properties.Resources.Autumn_Pants;
                                break;
                            case "Summer_Shorts":
                                parteAbajo.Image = DressDoll.Properties.Resources.Summer_Shorts;
                                break;
                        }
                        //poner a true  parte Abajo doll
                    }
                    else if (semantics.ContainsKey("partesArriba"))
                    {
                        String ropa = ((string)semantics["partesArriba"].Value);
                        if (doll.conflictoRopa("ParteArriba"))
                        {
                            //quitar parte parteEntera.Image
                            //poner a false en doll
                        }
                        switch (ropa)
                        {
                            case "Spring_Cardigan":
                                parteArriba.Image = DressDoll.Properties.Resources.Spring_Cardigan;
                                break;
                            case "Spring_Jacket":
                                parteArriba.Image = DressDoll.Properties.Resources.Spring_Jacket;
                                break;
                            case "Autumn_Coat":
                                parteArriba.Image = DressDoll.Properties.Resources.Autumn_Coat;
                                break;
                        }
                        //poner a true  parte Arriba doll
                    }
                    else if (semantics.ContainsKey("zapatos"))
                    {
                        String ropa = ((string)semantics["zapatos"].Value);
                        switch (ropa)
                        {
                            case "Summer_Shoes":
                                zapatos.Image = DressDoll.Properties.Resources.Summer_Shoes;
                                break;
                            case "Autumn_Boots":
                                zapatos.Image = DressDoll.Properties.Resources.Autumn_Boots;
                                break;
                        }
                    }
                    else if (semantics.ContainsKey("parteEntera")) 
                    {
                        String ropa = ((string)semantics["parteEntera"].Value);
                        if (doll.conflictoRopa("ParteEntera"))
                        {
                            //quitar parte  Arriba y abajo parteEntera.Image
                            //poner a false las dos en doll
                        }
                        switch (ropa)
                        {
                            case "Summer_Dress":
                                parteEntera.Image = DressDoll.Properties.Resources.Summer_Shoes;
                                break;
                            case "Summer_Swimsuit_1":
                                parteEntera.Image = DressDoll.Properties.Resources.Summer_Swimsuit_1;
                                break;
                            case "Summer_Swimsuit_2":
                                parteEntera.Image = DressDoll.Properties.Resources.Summer_Swimsuit_2;
                                break;
                            case "TrajeFestivo":
                                parteEntera.Image = DressDoll.Properties.Resources.TrajeFestivo;
                                this.BackgroundImage = DressDoll.Properties.Resources.navidad;
                                //musica
                                break;                              
                        }
                        // poner a true  parte Entera doll
                    }

                }
            }
            else {
                synth.Speak("entrando a lugar");
                String lugar = ((string)semantics["Lugares"].Value);
                switch (lugar)
                {
                    case "playa":
                        this.BackgroundImage = DressDoll.Properties.Resources.playa;
                        break;
                    case "parque":
                        this.BackgroundImage = DressDoll.Properties.Resources.parque;
                        break;
                }
            }

           /* if (!semantics.ContainsKey("rgb"))
            {
                this.label1.Text = "No info provided.";
            }
            else
            {
                this.label1.Text = rawText;
                this.BackColor = Color.FromArgb((int)semantics["rgb"].Value);
                Update();
                //synth.Speak(rawText);
            }*/

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
                   new SemanticResultValue("Pantalón", "Autumn_Pants");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesAbajoChoice.Add(resultValueBuilder);

            choiceResultValue =
                   new SemanticResultValue("Shorts", "Summer_Shorts");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesAbajoChoice.Add(resultValueBuilder);
            // Fin partes de abajo

            // Partes de arriba 
            choiceResultValue = new SemanticResultValue("Camiseta", "Spring_Cardigan");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesArribaChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Chaqueta", "Spring_Jacket");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesArribaChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Abrigo", "Autumn_Coat");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            partesArribaChoice.Add(resultValueBuilder);
            // Fin partes de arriba

            // Partes entera
            choiceResultValue = new SemanticResultValue("Vestido", "Summer_Dress");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            parteEnteraChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Festivo", "TrajeFestivo");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            parteEnteraChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Bañador", "Summer_Swimsuit_1");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            parteEnteraChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Bikini", "Summer_Swimsuit_2");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            parteEnteraChoice.Add(resultValueBuilder);
            // Fin partes enteras

            // Zapatos
            choiceResultValue = new SemanticResultValue("Zapatos", "Summer_Shoes");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            zapatosChoice.Add(resultValueBuilder);

            choiceResultValue = new SemanticResultValue("Botas", "Autumn_Boots");
            resultValueBuilder = new GrammarBuilder(choiceResultValue);
            zapatosChoice.Add(resultValueBuilder);
            // Fin zapatos

            //Creación de palabras claves ropa
            SemanticResultKey choiceResultKeyArriba = new SemanticResultKey("partesArriba", partesArribaChoice);
            SemanticResultKey choiceResultKeyAbajo = new SemanticResultKey("partesAbajo", partesAbajoChoice );
            SemanticResultKey choiceResultKeyZapatos = new SemanticResultKey("zapatos", zapatosChoice);
            SemanticResultKey choiceResultKeyEntero = new SemanticResultKey("parteEntera", parteEnteraChoice);
            
            //Creación de grammarbuilder ropa
            GrammarBuilder partesArriba = new GrammarBuilder(choiceResultKeyArriba);
            GrammarBuilder partesAbajo = new GrammarBuilder(choiceResultKeyAbajo);
            GrammarBuilder zapatos = new GrammarBuilder(choiceResultKeyZapatos);
            GrammarBuilder partesEnteras = new GrammarBuilder(choiceResultKeyEntero);

            Choices opcionesRopa = new Choices(new GrammarBuilder[] { partesArriba, partesAbajo, zapatos, partesEnteras });


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

            SemanticResultKey choiceResultKeyLugares = new SemanticResultKey("Lugares", lugaresChoice);

            GrammarBuilder lugares = new GrammarBuilder(choiceResultKeyLugares);

            //FRASES
            GrammarBuilder poner = "Poner";
            GrammarBuilder cambiar = "Cambiar";

            Choices cambiarAlternativa = new Choices(poner, cambiar);   
            SemanticResultKey choiceResultKeyCambiar = new SemanticResultKey("Cambiar", cambiarAlternativa);
            GrammarBuilder cambiarFrase = new GrammarBuilder(choiceResultKeyCambiar);
            cambiarFrase.Append(opcionesRopa);

            GrammarBuilder quitarFrase = new GrammarBuilder("Quitar");
            quitarFrase.Append(opcionesRopa);

            GrammarBuilder fondoFrase = new GrammarBuilder("Ir a");
            fondoFrase.Append(lugares);

            Choices opcionesFrase = new Choices(new GrammarBuilder[] {cambiarFrase, quitarFrase, fondoFrase});
            Grammar grammar = new Grammar((GrammarBuilder)opcionesFrase);
            grammar.Name = "Poner/Cambiar";
            return grammar;

            //Grammar grammar = new Grammar("so.xml.txt");
        }

    }
}
