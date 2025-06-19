string orderStream = "B123,C234,A345,C15,B177,G3003,C235,B179";

string[] splitOrder = orderStream.Split(",");
Array.Sort(splitOrder);

for (int i = 0; i < splitOrder.Length; i++)
{
    string message = "";
    if (splitOrder[i].ToCharArray().Length != 4)
    {
        message = "\t- Error";
    }
    splitOrder[i] += message;
    Console.WriteLine(splitOrder[i]);
}

// string pangram = "The quick brown fox jumps over the lazy dog";
// string[] splitPangram = pangram.Split();

// for (int i = 0; i < splitPangram.Length; i++)
// {
//     char[] split = splitPangram[i].ToCharArray(); // char array 
//     Array.Reverse(split);

//     splitPangram[i] = new string(split);                       
// }

// pangram = string.Join(' ', splitPangram);
// Console.WriteLine(pangram);

// // the ourAnimals array will store the following: 
// using System.Xml;

// string animalSpecies = "";
// string animalID = "";
// string animalAge = "";
// string animalPhysicalDescription = "";
// string animalPersonalityDescription = "";
// string animalNickname = "";

// // variables that support data entry
// int maxPets = 8;
// string? readResult;
// string menuSelection = "";

// // array used to store runtime data, there is no persisted data
// string[,] ourAnimals = new string[maxPets, 6];

// // TODO: Convert the if-elseif-else construct to a switch statement

// // create some initial ourAnimals array entries
// for (int i = 0; i < maxPets; i++)
// {
//     switch (i)
//     {
//         case 0:
//             animalSpecies = "dog";
//             animalID = "d1";
//             animalAge = "2";
//             animalPhysicalDescription = "medium sized cream colored female golden retriever weighing about 65 pounds. housebroken.";
//             animalPersonalityDescription = "loves to have her belly rubbed and likes to chase her tail. gives lots of kisses.";
//             animalNickname = "lola";
//             break;
//         case 1:
//             animalSpecies = "dog";
//             animalID = "d2";
//             animalAge = "9";
//             animalPhysicalDescription = "large reddish-brown male golden retriever weighing about 85 pounds. housebroken.";
//             animalPersonalityDescription = "loves to have his ears rubbed when he greets you at the door, or at any time! loves to lean-in and give doggy hugs.";
//             animalNickname = "loki";
//             break;
//         case 2:
//             animalSpecies = "cat";
//             animalID = "c3";
//             animalAge = "1";
//             animalPhysicalDescription = "small white female weighing about 8 pounds. litter box trained.";
//             animalPersonalityDescription = "friendly";
//             animalNickname = "Puss";
//             break;
//         case 3:
//             animalSpecies = "cat";
//             animalID = "c4";
//             animalAge = "?";
//             animalPhysicalDescription = "";
//             animalPersonalityDescription = "";
//             animalNickname = "";
//             break;
//         default:
//             animalSpecies = "";
//             animalID = "";
//             animalAge = "";
//             animalPhysicalDescription = "";
//             animalPersonalityDescription = "";
//             animalNickname = "";
//             break;
//     }

//     ourAnimals[i, 0] = "ID #: " + animalID;
//     ourAnimals[i, 1] = "Species: " + animalSpecies;
//     ourAnimals[i, 2] = "Age: " + animalAge;
//     ourAnimals[i, 3] = "Nickname: " + animalNickname;
//     ourAnimals[i, 4] = "Physical description: " + animalPhysicalDescription;
//     ourAnimals[i, 5] = "Personality: " + animalPersonalityDescription;
// }

// // display the top-level menu options
// do
// {
//     Console.Clear();

//     Console.WriteLine("Welcome to the Contoso PetFriends app. Your main menu options are:");
//     Console.WriteLine(" 1. List all of our current pet information");
//     Console.WriteLine(" 2. Add a new animal friend to the ourAnimals array");
//     Console.WriteLine(" 3. Ensure animal ages and physical descriptions are complete");
//     Console.WriteLine(" 4. Ensure animal nicknames and personality descriptions are complete");
//     Console.WriteLine(" 5. Edit an animal’s age");
//     Console.WriteLine(" 6. Edit an animal’s personality description");
//     Console.WriteLine(" 7. Display all cats with a specified characteristic");
//     Console.WriteLine(" 8. Display all dogs with a specified characteristic");
//     Console.WriteLine();
//     Console.WriteLine("Enter your selection number (or type Exit to exit the program)");

//     readResult = Console.ReadLine();
//     if (readResult != null)
//     {
//         menuSelection = readResult.ToLower();
//     }
//     Console.WriteLine($"You selected menu option {menuSelection}.");
//     switch (menuSelection)
//     {
//         case "1":
//             for (int i = 0; i < maxPets; i++)
//             {
//                 if (ourAnimals[i, 0] != "ID #: ")
//                 {
//                     Console.WriteLine();

//                     for (int j = 0; j < 6; j++)
//                     {
//                         Console.WriteLine(ourAnimals[i, j]);
//                     }
//                 }
//             }
//             break;
//         case "2":
//             string addpet = "y";
//             int petCount = 0;

//             for (int i = 0; i < maxPets; i++)
//             {
//                 if (ourAnimals[i, 0] != "ID #: ")
//                 {
//                     petCount++;
//                 }
//             }
//             if (petCount < maxPets)
//             {
//                 Console.WriteLine($"We currently have {petCount} pets that need homes. We can manage {(maxPets - petCount)} more.");
//             }

//             while (addpet == "y" && petCount < maxPets)
//             {
//                 // 1. Pet ID
//                 bool validEntry = false;
//                 do
//                 {
//                     Console.WriteLine("dog or cat?");
//                     readResult = Console.ReadLine();
//                     if (readResult != null)
//                     {
//                         animalSpecies = readResult.ToLower();
//                         if (animalSpecies != "dog" && animalSpecies != "cat")
//                         {
//                             Console.WriteLine("Unreognized species");
//                             validEntry = false;
//                         }
//                         else
//                         {
//                             validEntry = true;
//                         }
//                     }

//                 } while (validEntry == false);
//                 animalID = animalSpecies.Substring(0, 1) + (petCount + 1).ToString();

//                 // 2. Pet age 
//                 do
//                 {
//                     int petage;
//                     Console.WriteLine("Enter age or type '?': ");
//                     readResult = Console.ReadLine();
//                     if (readResult != null)
//                     {
//                         animalAge = readResult;
//                         if (animalAge != "?")
//                             validEntry = (bool)int.TryParse(animalAge, out petage);
//                         else
//                             validEntry = true;
//                     }
//                 } while (validEntry == false);

//                 // 3. Physical data 
//                 do
//                 {
//                     Console.WriteLine("Enter (size, color, gender, weight, housebroken): ");
//                     readResult = Console.ReadLine();
//                     if (readResult != null)
//                     {
//                         animalPhysicalDescription = readResult.ToLower();
//                         if (animalPhysicalDescription == "")
//                             animalPhysicalDescription = "tbd";
//                     }

//                 } while (animalPhysicalDescription == "");

//                 // 4. Personality 
//                 do
//                 {
//                     Console.WriteLine("Enter personality: ");
//                     readResult = Console.ReadLine();
//                     if (readResult != null)
//                     {
//                         animalPersonalityDescription = readResult.ToLower();
//                         if (animalPersonalityDescription == "")
//                             animalPersonalityDescription = "tbd";
//                     }
//                 } while (animalPersonalityDescription == "");

//                 // 5. Nickname
//                 do
//                 {
//                     Console.WriteLine("Enter nickname: ");
//                     readResult = Console.ReadLine();
//                     if (readResult != null)
//                     {
//                         animalNickname = readResult.ToLower();
//                         if (animalNickname == "")
//                             animalNickname = "tbd";
//                     }
//                 } while (animalNickname == "");

//                 ourAnimals[petCount, 0] = "ID #: " + animalID;
//                 ourAnimals[petCount, 1] = "Species: " + animalSpecies;
//                 ourAnimals[petCount, 2] = "Age: " + animalAge;
//                 ourAnimals[petCount, 3] = "Nickname: " + animalNickname;
//                 ourAnimals[petCount, 4] = "Physical description: " + animalPhysicalDescription;
//                 ourAnimals[petCount, 5] = "Personality: " + animalPersonalityDescription;

//                 petCount++;
//                 if (petCount < maxPets)
//                 {
//                     Console.WriteLine("Add more pet? y/n");
//                     do
//                     {
//                         readResult = Console.ReadLine();
//                         if (readResult != null)
//                             addpet = readResult.ToLower();
//                     } while (addpet != "y" && addpet != "n");
//                 }

//             }

//             if (petCount >= maxPets)
//             {
//                 Console.WriteLine("We have reached our limit on the number of pets that we can manage.");
//             }
//             break;
//         case "3":
//             for (int i = 0; i < maxPets; i++)
//             {
//                 if (ourAnimals[i, 0] != "ID #: ")
//                 {
//                     // Fix age
//                     if (ourAnimals[i, 2] == "Age: " || ourAnimals[i, 2].Contains('?'))
//                     {
//                         bool validEntry = false;

//                         do
//                         {
//                             Console.WriteLine(ourAnimals[i, 0]);
//                             Console.WriteLine("Enter correct age: ");
//                             readResult = Console.ReadLine();

//                             if (readResult != null)
//                             {
//                                 animalAge = readResult;
//                                 int age;
//                                 validEntry = int.TryParse(animalAge, out age);
//                             }

//                         } while (validEntry == false || animalAge == "" || animalAge.Contains('?'));

//                         ourAnimals[i, 2] = "Age: " + animalAge;
//                         Console.WriteLine("Age " + animalAge + " is valid.");
//                     }

//                     // if ()
//                     // {

//                     // }
//                     /*
//                     ourAnimals[petCount, 0] = "ID #: " + animalID;
//                     ourAnimals[petCount, 1] = "Species: " + animalSpecies;
//                     ourAnimals[petCount, 2] = "Age: " + animalAge;
//                     ourAnimals[petCount, 3] = "Nickname: " + animalNickname;
//                     ourAnimals[petCount, 4] = "Physical description: " + animalPhysicalDescription;
//                     ourAnimals[petCount, 5] = "Personality: "
//                     */
//                 }
//             }
//             Console.WriteLine("Ages and Description are valid");
//             break;
//         case "4":
//             Console.WriteLine("Challenge Project - please check back soon to see progress.");
//             break;
//         case "5":
//         case "6":
//         case "7":
//         case "8":
//             Console.WriteLine("UNDER CONSTRUCTION - please check back next month to see progress.");
//             break;
//         default:
//             break;

//     }
//     Console.WriteLine("Press the Enter key to continue");

//     // pause code execution
//     readResult = Console.ReadLine();

// } while (menuSelection != "exit");




