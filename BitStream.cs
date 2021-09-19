using System;
using System.Text;
using System.Collections.Generic;

public class BitStream
{
    List<byte> WritenBytes;
    private byte[] bytes;
    private int readPos = 0;

    public BitStream(byte[] bytes)
    {
        this.bytes = bytes;
        WritenBytes = new List<byte>();
    }
    public BitStream()
    {
        bytes = new byte[1];
        WritenBytes = new List<byte>();
    }

    private byte[] Read(int bytes, bool move = true)
    {
        if ((this.bytes.Length - readPos) < bytes)
            return new byte[bytes];

        byte[] ReadBytes = new byte[bytes];

        for (int i = 0; i < bytes; i++)
        {
            ReadBytes[i] = this.bytes[readPos + i];
        }
        if (move)
            readPos += bytes;

        return ReadBytes;
    }

    public byte ReadByte(bool move = true)
    {
        return Read(1, move).First();
    }
    public byte[] ReadBytes(int bytesToRead, bool move = true)
    {
        return Read(bytesToRead, move);
    }
    public short ReadShort(bool move = true)
    {
        return BitConverter.ToInt16(Read(2, move), 0);
    }
    public ushort ReadUShort(bool move = true)
    {
        return BitConverter.ToUInt16(Read(2, move), 0);
    }
    public int ReadInt(bool move = true)
    {
        return BitConverter.ToInt32(Read(4, move), 0);
    }
    public uint ReadUInt(bool move = true)
    {
        return BitConverter.ToUInt32(Read(4, move), 0);
    }
    public long ReadLong(bool move = true)
    {
        return BitConverter.ToInt64(Read(8, move), 0);
    }
    public ulong ReadULong(bool move = true)
    {
        return BitConverter.ToUInt64(Read(8, move), 0);
    }
    public float ReadFloat(bool move = true)
    {
        return BitConverter.ToSingle(Read(4, move), 0);
    }
    public double ReadDouble(bool move = true)
    {
        return BitConverter.ToDouble(Read(8, move), 0);
    }
    public bool ReadBool(bool move = true)
    {
        return BitConverter.ToBoolean(Read(1, move), 0);
    }
    public string ReadString(bool move = true)
    {
        int myReadPos = readPos;

        int StrLen = ReadInt(move);

        myReadPos += 4;

        //Console.WriteLine("StrLen: "+StrLen+" ? readpos: "+readPos);
        string str = Encoding.UTF8.GetString(bytes, myReadPos, StrLen);

        if (move)
            readPos += StrLen;

        return str;
    }

    public void Write(byte v)
    {
        WritenBytes.Add(v);
    }
    public void Write(byte[] v)
    {
        WritenBytes.AddRange(v);
    }
    public void Write(short v)
    {
        WritenBytes.AddRange(BitConverter.GetBytes(v));
    }
    public void Write(ushort v)
    {
        WritenBytes.AddRange(BitConverter.GetBytes(v));
    }
    public void Write(int v)
    {
        WritenBytes.AddRange(BitConverter.GetBytes(v));
    }
    public void Write(uint v)
    {
        WritenBytes.AddRange(BitConverter.GetBytes(v));
    }
    public void Write(long v)
    {
        WritenBytes.AddRange(BitConverter.GetBytes(v));
    }
    public void Write(ulong v)
    {
        WritenBytes.AddRange(BitConverter.GetBytes(v));
    }
    public void Write(float v)
    {
        WritenBytes.AddRange(BitConverter.GetBytes(v));
    }
    public void Write(double v)
    {
        WritenBytes.AddRange(BitConverter.GetBytes(v));
    }
    public void Write(bool v)
    {
        WritenBytes.AddRange(BitConverter.GetBytes(v));
    }
    public void Write(string v)
    {
        byte[] stringL = Encoding.UTF8.GetBytes(v);
        this.Write(stringL.Length);
        WritenBytes.AddRange(stringL);
    }

    public void SetAsData()
    {
        bytes = WritenBytes.ToArray();
        readPos = 0;
        WritenBytes.Clear();
    }
    public byte[] GetBytes()
    {
        return bytes;
    }
    public byte[] GetWritenBytes()
    {
        return WritenBytes.ToArray();
    }

    private string ToBase16(int num)
    {
        const string Base = "0123456789ABCDEF";
        string result = "";
        while (num != 0)
        {
            result += Base[num % 16];
            num /= 16;
        }

        if (result == "")
            result = "00";

        if (result.Length == 1)
            result = "0" + result;

        return result + " ";
    }

    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < bytes.Length; i++)
        {
            result += ToBase16(bytes[i]);
        }
        return result;
    }
}
