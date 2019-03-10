using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

// @jlvoiseux
// A class that draws a molecule, unsing atom names, positions, and bonding information
public class MoleculeDrawer : MonoBehaviour {

    List<string[]> sizeArray = new List<string[]>();
    List<string[]> atomArray = new List<string[]>();
    List<string[]> bondArray = new List<string[]>();

    public GameObject molecule;
    public GameObject atom;
    public GameObject bondParent;
    public GameObject bondCylinder;
    public float atomSizeConst = 1;
    public float bondSizeConst = 0.1f;
    public float doubleBondGap = 0.1f;
    public float tripleBondGap = 0.1f;

    // Test .mol file
    String data = "https://cactus.nci.nih.gov/chemical/structure/ClS(Cl)(=%5BCl+%5D)=%5BCl+%5D/f...\n__Jmol-14_07131816303D 1   1.00000     0.00000     0\nJmol version 14.29.17  2018-06-15 14:15 EXTRACT: ({0:4})\n  0  0  0  0  0  0            999 V3000\nM  V30 BEGIN CTAB\nM  V30 COUNTS 5 4 0 0 0\nM  V30 BEGIN ATOM\nM  V30 1 Cl      0.00000     -1.54460     -1.09220 0\nM  V30 2 S      0.00000      0.00000      0.00000 0\nM  V30 3 Cl      0.00000      1.54460     -1.09220 0\nM  V30 4 Cl     -1.54460      0.00000      1.09220 0 CHG=1\nM  V30 5 Cl      1.54460      0.00000      1.09220 0 CHG=1\nM  V30 END ATOM\nM  V30 BEGIN BOND\nM  V30 1 1 1 2\nM  V30 2 1 2 3\nM  V30 3 2 2 4\nM  V30 4 2 2 5\nM  V30 END BOND\nM  V30 END CTAB\nM  END\n";

    // Use this for initialization
    void Start() {
        sizeArray.Add(new string[] { "H", "0.693147181" });
        sizeArray.Add(new string[] { "He", "1.098612289" });
        sizeArray.Add(new string[] { "Li", "1.386294361" });
        sizeArray.Add(new string[] { "Be", "1.609437912" });
        sizeArray.Add(new string[] { "B", "1.791759469" });
        sizeArray.Add(new string[] { "C", "1.945910149" });
        sizeArray.Add(new string[] { "N", "2.079441542" });
        sizeArray.Add(new string[] { "O", "2.197224577" });
        sizeArray.Add(new string[] { "F", "2.302585093" });
        sizeArray.Add(new string[] { "Ne", "2.397895273" });
        sizeArray.Add(new string[] { "Na", "2.48490665" });
        sizeArray.Add(new string[] { "Mg", "2.564949357" });
        sizeArray.Add(new string[] { "Al", "2.63905733" });
        sizeArray.Add(new string[] { "Si", "2.708050201" });
        sizeArray.Add(new string[] { "P", "2.772588722" });
        sizeArray.Add(new string[] { "S", "2.833213344" });
        sizeArray.Add(new string[] { "Cl", "2.890371758" });
        sizeArray.Add(new string[] { "Ar", "2.944438979" });
        sizeArray.Add(new string[] { "K", "2.995732274" });
        sizeArray.Add(new string[] { "Ca", "3.044522438" });
        sizeArray.Add(new string[] { "Sc", "3.091042453" });
        sizeArray.Add(new string[] { "Ti", "3.135494216" });
        sizeArray.Add(new string[] { "V", "3.17805383" });
        sizeArray.Add(new string[] { "Cr", "3.218875825" });
        sizeArray.Add(new string[] { "Mn", "3.258096538" });
        sizeArray.Add(new string[] { "Fe", "3.295836866" });
        sizeArray.Add(new string[] { "Co", "3.33220451" });
        sizeArray.Add(new string[] { "Ni", "3.36729583" });
        sizeArray.Add(new string[] { "Cu", "3.401197382" });
        sizeArray.Add(new string[] { "Zn", "3.433987204" });
        sizeArray.Add(new string[] { "Ga", "3.465735903" });
        sizeArray.Add(new string[] { "Ge", "3.496507561" });
        sizeArray.Add(new string[] { "As", "3.526360525" });
        sizeArray.Add(new string[] { "Se", "3.555348061" });
        sizeArray.Add(new string[] { "Br", "3.583518938" });
        sizeArray.Add(new string[] { "Kr", "3.610917913" });
        sizeArray.Add(new string[] { "Rb", "3.63758616" });
        sizeArray.Add(new string[] { "Sr", "3.663561646" });
        sizeArray.Add(new string[] { "Y", "3.688879454" });
        sizeArray.Add(new string[] { "Zr", "3.713572067" });
        sizeArray.Add(new string[] { "Nb", "3.737669618" });
        sizeArray.Add(new string[] { "Mo", "3.761200116" });
        sizeArray.Add(new string[] { "Tc", "3.784189634" });
        sizeArray.Add(new string[] { "Ru", "3.80666249" });
        sizeArray.Add(new string[] { "Rh", "3.828641396" });
        sizeArray.Add(new string[] { "Pd", "3.850147602" });
        sizeArray.Add(new string[] { "Ag", "3.871201011" });
        sizeArray.Add(new string[] { "Cd", "3.891820298" });
        sizeArray.Add(new string[] { "In", "3.912023005" });
        sizeArray.Add(new string[] { "Sn", "3.931825633" });
        sizeArray.Add(new string[] { "Sb", "3.951243719" });
        sizeArray.Add(new string[] { "Te", "3.970291914" });
        sizeArray.Add(new string[] { "I", "3.988984047" });
        sizeArray.Add(new string[] { "Xe", "4.007333185" });
        sizeArray.Add(new string[] { "Cs", "4.025351691" });
        sizeArray.Add(new string[] { "Ba", "4.043051268" });
        sizeArray.Add(new string[] { "La", "4.060443011" });
        sizeArray.Add(new string[] { "Ce", "4.077537444" });
        sizeArray.Add(new string[] { "Pr", "4.094344562" });
        sizeArray.Add(new string[] { "Nd", "4.110873864" });
        sizeArray.Add(new string[] { "Pm", "4.127134385" });
        sizeArray.Add(new string[] { "Sm", "4.143134726" });
        sizeArray.Add(new string[] { "Eu", "4.158883083" });
        sizeArray.Add(new string[] { "Gd", "4.17438727" });
        sizeArray.Add(new string[] { "Tb", "4.189654742" });
        sizeArray.Add(new string[] { "Dy", "4.204692619" });
        sizeArray.Add(new string[] { "Ho", "4.219507705" });
        sizeArray.Add(new string[] { "Er", "4.234106505" });
        sizeArray.Add(new string[] { "Tm", "4.248495242" });
        sizeArray.Add(new string[] { "Yb", "4.262679877" });
        sizeArray.Add(new string[] { "Lu", "4.276666119" });
        sizeArray.Add(new string[] { "Hf", "4.290459441" });
        sizeArray.Add(new string[] { "Ta", "4.304065093" });
        sizeArray.Add(new string[] { "W", "4.317488114" });
        sizeArray.Add(new string[] { "Re", "4.33073334" });
        sizeArray.Add(new string[] { "Os", "4.343805422" });
        sizeArray.Add(new string[] { "Ir", "4.356708827" });
        sizeArray.Add(new string[] { "Pt", "4.369447852" });
        sizeArray.Add(new string[] { "Au", "4.382026635" });
        sizeArray.Add(new string[] { "Hg", "4.394449155" });
        sizeArray.Add(new string[] { "Tl", "4.406719247" });
        sizeArray.Add(new string[] { "Pb", "4.418840608" });
        sizeArray.Add(new string[] { "Bi", "4.430816799" });
        sizeArray.Add(new string[] { "Po", "4.442651256" });
        sizeArray.Add(new string[] { "At", "4.454347296" });
        sizeArray.Add(new string[] { "Rn", "4.465908119" });
        sizeArray.Add(new string[] { "Fr", "4.477336814" });
        sizeArray.Add(new string[] { "Ra", "4.48863637" });
        sizeArray.Add(new string[] { "Ac", "4.49980967" });
        sizeArray.Add(new string[] { "Th", "4.510859507" });
        sizeArray.Add(new string[] { "Pa", "4.521788577" });
        sizeArray.Add(new string[] { "U", "4.532599493" });
        sizeArray.Add(new string[] { "Np", "4.543294782" });
        sizeArray.Add(new string[] { "Pu", "4.553876892" });
        sizeArray.Add(new string[] { "Am", "4.564348191" });
        sizeArray.Add(new string[] { "Cm", "4.574710979" });
        sizeArray.Add(new string[] { "Bk", "4.584967479" });
        sizeArray.Add(new string[] { "Cf", "4.59511985" });
        sizeArray.Add(new string[] { "Es", "4.605170186" });
        sizeArray.Add(new string[] { "Fm", "4.615120517" });
        sizeArray.Add(new string[] { "Md", "4.624972813" });
        sizeArray.Add(new string[] { "No", "4.634728988" });
        sizeArray.Add(new string[] { "Lr", "4.644390899" });
        sizeArray.Add(new string[] { "Rf", "4.65396035" });
        sizeArray.Add(new string[] { "Db", "4.663439094" });
        sizeArray.Add(new string[] { "Sg", "4.672828834" });
        sizeArray.Add(new string[] { "Bh", "4.682131227" });
        sizeArray.Add(new string[] { "Hs", "4.691347882" });
        sizeArray.Add(new string[] { "Mt", "4.700480366" });
        sizeArray.Add(new string[] { "Ds", "4.709530201" });
        sizeArray.Add(new string[] { "Rg", "4.718498871" });
        sizeArray.Add(new string[] { "Cn", "4.727387819" });
        sizeArray.Add(new string[] { "Nh", "4.736198448" });
        sizeArray.Add(new string[] { "Fl", "4.744932128" });
        sizeArray.Add(new string[] { "Mc", "4.753590191" });
        sizeArray.Add(new string[] { "Lv", "4.762173935" });
        sizeArray.Add(new string[] { "Ts", "4.770684624" });
        sizeArray.Add(new string[] { "Og", "4.779123493" });

        parseMolFile(GameObject.Find("UserData").GetComponent<UserData>().chosen_mol);

        // Atom instantiation
        foreach(var item in atomArray) {
            GameObject newAtom = Instantiate(atom, new Vector3(float.Parse(item[1]), float.Parse(item[2]), float.Parse(item[3])), new Quaternion(0, 0, 0, 0), molecule.transform);
            newAtom.transform.localScale = getAtomScale(item[0], atomSizeConst);
        }

        // Bond instantiation
        foreach (var item in bondArray) {
            Vector3[] bondPos = getBondPos(Int32.Parse(item[1]), Int32.Parse(item[2]));
            GameObject newBondParent = Instantiate(bondParent, molecule.transform);
            for(int i = 0; i < Int32.Parse(item[0]); i++) {
                GameObject newBond = Instantiate(bondParent, newBondParent.transform);
                GameObject newBond1 = Instantiate(bondCylinder, bondPos[0], Quaternion.LookRotation(Vector3.Cross(Vector3.up, bondPos[2].normalized).normalized, bondPos[2].normalized), molecule.transform);
                newBond1.transform.localScale = new Vector3(bondSizeConst, bondPos[2].magnitude / 4, bondSizeConst);
                GameObject newBond2 = Instantiate(bondCylinder, bondPos[1], Quaternion.LookRotation(Vector3.Cross(Vector3.up, bondPos[2].normalized).normalized, bondPos[2].normalized), molecule.transform);
                newBond2.transform.localScale = new Vector3(bondSizeConst, bondPos[2].magnitude / 4, bondSizeConst);

                if(Int32.Parse(item[0]) == 2 && i == 0) {
                    newBond1.transform.position += Vector3.Cross(Vector3.up, bondPos[2].normalized).normalized * doubleBondGap;
                    newBond2.transform.position += Vector3.Cross(Vector3.up, bondPos[2].normalized).normalized * doubleBondGap;
                }
                else if (Int32.Parse(item[0]) == 2 && i == 1) {
                    newBond1.transform.position -= Vector3.Cross(Vector3.up, bondPos[2].normalized).normalized * doubleBondGap;
                    newBond2.transform.position -= Vector3.Cross(Vector3.up, bondPos[2].normalized).normalized * doubleBondGap;
                }
                else if (Int32.Parse(item[0]) == 3 && i == 1) {
                    newBond1.transform.position += Vector3.Cross(Vector3.up, bondPos[2].normalized).normalized * tripleBondGap;
                    newBond2.transform.position += Vector3.Cross(Vector3.up, bondPos[2].normalized).normalized * tripleBondGap;
                }
                else if (Int32.Parse(item[0]) == 3 && i == 2) {
                    newBond1.transform.position += Vector3.Cross(Vector3.up, bondPos[2].normalized).normalized * tripleBondGap;
                    newBond2.transform.position += Vector3.Cross(Vector3.up, bondPos[2].normalized).normalized * tripleBondGap;
                }

                newBond1.transform.parent = newBond.transform;
                newBond2.transform.parent = newBond.transform;
            }
        }
        bondArray.ForEach(Console.WriteLine);
    }

    Vector3[] getBondPos(int atom1, int atom2) {
        Vector3 pos1 = new Vector3(float.Parse(atomArray[atom1 - 1][1]), float.Parse(atomArray[atom1 - 1][2]), float.Parse(atomArray[atom1 - 1][3])) + molecule.transform.position;
        Vector3 pos2 = new Vector3(float.Parse(atomArray[atom2 - 1][1]), float.Parse(atomArray[atom2 - 1][2]), float.Parse(atomArray[atom2 - 1][3])) + molecule.transform.position;
        Vector3 mid = (pos1 + pos2) / 2;
        return new Vector3[] { mid + (pos2 - pos1) / 4, mid - (pos2 - pos1) / 4, pos1-pos2};
    }

    Vector3 getAtomScale(string name, float sizeConst) {
        foreach (var item in sizeArray) {
            if (item[0] == name) {
                return new Vector3(float.Parse(item[1]), float.Parse(item[1]), float.Parse(item[1]))/sizeConst;
            }
        }
        return new Vector3(0, 0, 0);
    }

    void parseMolFile(string molFile) {

        bool initFlag = false;
        int countAtom = 0;
        bool atomFlag = false;
        int countBond = 0;
        bool bondFlag = false;
        int count = 0;

        foreach (var line in molFile.Split(new char[] {'\n'})){
            if(line.Contains("BEGIN CTAB")){                
                initFlag = true;                
            }
            else if (line.Contains("BEGIN ATOM")) {
                atomFlag = true;                
            }
            else if (line.Contains("BEGIN BOND")) {
                bondFlag = true;
            }
            else if (initFlag) {                
                countAtom = Int32.Parse(Regex.Split(line, @"\s+")[3]);
                countBond = Int32.Parse(Regex.Split(line, @"\s+")[4]);                
                initFlag = false;
            }
            else if (atomFlag) {
                if (count < countAtom) {
                    atomArray.Add(new string[] {Regex.Split(line, @"\s+")[3], Regex.Split(line, @"\s+")[4], Regex.Split(line, @"\s+")[5], Regex.Split(line, @"\s+")[6]});
                    count += 1;
                }
                else {
                    atomFlag = false;
                    count = 0;
                }
            }
            else if (bondFlag) {
                if (count < countBond) {
                    bondArray.Add(new string[] { Regex.Split(line, @"\s+")[3], Regex.Split(line, @"\s+")[4], Regex.Split(line, @"\s+")[5]});
                    count += 1;
                }
                else {
                    bondFlag = false;
                    count = 0;
                }
            }
        }
    }   
}
