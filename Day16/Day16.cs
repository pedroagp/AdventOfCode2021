using AdventOfCode2021.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Challenges {
    internal class Day16 : Challenge {
        readonly string binaryStr;
        readonly packet mainPacket;
        
        public struct packet {
            public int version;
            public int type;
            public long literal;
            public List<packet> subpackets = new();
        }
        public Day16(bool testInput = false) : base(testInput) {
            binaryStr = String.Join("", base.input[0].Select(x => Convert.ToString(Convert.ToInt32(x.ToString(), 16), 2).PadLeft(4, '0')));
            
            int ignore = 0; //only needed for recursion
            mainPacket = Parse(0, ref ignore);
        }

        public override string FirstResult() {
            return $"{getVersionSum(mainPacket)}";
        }

        public long getVersionSum(packet p) {
            return p.version + p.subpackets.Sum(x => getVersionSum(x));
        }

        public long GetValue(packet p) {
            switch (p.type) {
                case 0:
                    return p.subpackets.Sum(x => GetValue(x));
                case 1:
                    long init = 1;
                    return p.subpackets.Aggregate(init, (a, b) => a * GetValue(b));
                case 2:
                    return p.subpackets.Min(x => GetValue(x));
                case 3:
                    return p.subpackets.Max(x => GetValue(x));
                case 4:
                    return p.literal;
                case 5:
                    return GetValue(p.subpackets[0]) > GetValue(p.subpackets[1]) ? 1 : 0;
                case 6:
                    return GetValue(p.subpackets[0]) < GetValue(p.subpackets[1]) ? 1 : 0;
                case 7:
                    return GetValue(p.subpackets[0]) == GetValue(p.subpackets[1]) ? 1 : 0;
                default:
                    return 0xFAFE;

            }
        }

        public override string SecondResult() {

            return $"{GetValue(mainPacket)}";
        }

        private packet Parse(int start, ref int size) {
            var i = start; //i = reading index;
            packet p = new packet();
            p.version = Convert.ToInt32(binaryStr.Substring(i, 3), 2);
            i += 3;
            p.type = Convert.ToInt32(binaryStr.Substring(i, 3), 2);
            i += 3;

            var count = 0;
            switch (p.type) {
                case 4: //Literal
                    string literal = "";
                    //read blocks of 5 digits
                    while (binaryStr[i] == '1') { //valide if first is 1, if 0 end
                        i++;
                        literal += binaryStr.Substring(i, 4);
                        i += 4;
                    }
                    i++;
                    literal += binaryStr.Substring(i, 4);
                    i += 4;
                    p.literal = Convert.ToInt64(literal, 2);
                    break;
                default:
                    if (binaryStr[i] == '0') {  //15 bits = tamanho dos pacotes
                        i++;
                        var length = Convert.ToInt32(binaryStr.Substring(i, 15), 2);
                        i += 15;
                        while (count < length) {
                            p.subpackets.Add(Parse(i + count, ref count));
                        }
                    } else { // == '1' = 11 bits = nr dos pacotes
                        i++;
                        var total = Convert.ToInt32(binaryStr.Substring(i, 11), 2);
                        i += 11;
                        for (int j = 0; j < total; j++) {
                            p.subpackets.Add(Parse(i + count, ref count));
                        }
                    }
                    i += count;
                    break;
            }
            size += (i - start);
            return p;
        }


    }
}
