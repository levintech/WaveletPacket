﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncMsg
{
    class Salsa20
    {



        public sealed class Salsa20CryptoTransform : ICryptoTransform
        {
            // The ChaCha20 state 
            private uint[] state;
            private readonly int numRounds;

            // Construct a new Salsa20 state.
            public Salsa20CryptoTransform(byte[] key, byte[] iv)
            {
                if (key == null)
                {
                    throw new ArgumentNullException("key");
                }
                if (iv == null)
                {
                    throw new ArgumentNullException("iv");
                }
                if (key.Length != 32)
                {
                    throw new ArgumentException(
                        "Key length must be 32 bytes. Actual is " + key.Length.ToString()
                    );
                }
                if (iv.Length < 8)
                {
                    throw new ArgumentException(
                        "Nonce should have 8 bytes. Actual is " + iv.Length.ToString()
                    );
                }

                Initialize(key, iv);
                numRounds = 20;
            }

            public Salsa20CryptoTransform()
            {
            }

            // Initialize the Salsa state with the given key and nonce. A 32-byte 
            // (256-bit) key is required. The nonce must be at least 8-bytes 
            // (64-bits) long. If it is any larger, only the first 64 bits will be
            // used. 
            private void Initialize(byte[] key, byte[] iv)
            {
                state = new uint[16];

                state[1] = ToUInt32(key, 0);
                state[2] = ToUInt32(key, 4);
                state[3] = ToUInt32(key, 8);
                state[4] = ToUInt32(key, 12);

                byte[] sigma = Encoding.ASCII.GetBytes("expand 32-byte k");

                byte[] constants = sigma;
                int keyIndex = key.Length - 16;

                state[11] = ToUInt32(key, keyIndex + 0);
                state[12] = ToUInt32(key, keyIndex + 4);
                state[13] = ToUInt32(key, keyIndex + 8);
                state[14] = ToUInt32(key, keyIndex + 12);

                state[0] = ToUInt32(constants, 0);
                state[5] = ToUInt32(constants, 4);
                state[10] = ToUInt32(constants, 8);
                state[15] = ToUInt32(constants, 12);

                state[6] = ToUInt32(iv, 0);
                state[7] = ToUInt32(iv, 4);
                state[8] = 0;
                state[9] = 0;
            }

            // Transforms the specified region of the specified byte array. 
            public byte[] TransformFinalBlock(byte[] inputBuffer,
                int inputOffset, int inputCount)
            {

                // No parameter checking needed as that is handled in TransformBlock()
                byte[] output = new byte[inputCount];
                TransformBlock(inputBuffer, inputOffset, inputCount, output, 0);

                return output;
            }

            // Encrypt an arbitrary-length plaintext message (inputBuffer), writing the 
            // resulting ciphertext to the outputBuffer. The number of bytes to read 
            // from the input buffer is determined by inputCount.
            public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount,
                byte[] outputBuffer, int outputOffset)
            {

                /* Check the parameters */
                if (inputBuffer == null)
                {
                    throw new ArgumentNullException("inputBuffer");
                }
                if (inputOffset < 0 || inputOffset >= inputBuffer.Length)
                {
                    throw new ArgumentOutOfRangeException("inputOffset");
                }
                if (inputCount < 0 ||
                    (inputOffset + inputCount) > inputBuffer.Length)
                {
                    throw new ArgumentOutOfRangeException("inputCount");
                }
                if (outputBuffer == null)
                {
                    throw new ArgumentNullException("outputBuffer");
                }
                if (outputOffset < 0 ||
                    (outputOffset + inputCount) > outputBuffer.Length)
                {
                    throw new ArgumentOutOfRangeException("outputOffset");
                }
                if (state == null)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }

                byte[] output = new byte[64];
                int bytesTransformed = 0;

                while (inputCount > 0)
                {
                    Salsa20Core(output, state);

                    state[8] = AddOne(state[8]);
                    if (state[8] == 0)
                    {
                        /* Stopping at 2^70 bytes per nonce is the 
                         * user's responsibility 
                         */
                        state[9] = AddOne(state[9]);
                    }

                    int blockSize = Math.Min(64, inputCount);

                    for (int i = 0; i < blockSize; i++)
                    {
                        outputBuffer[outputOffset + i] =
                            (byte)(inputBuffer[inputOffset + i] ^ output[i]);
                    }

                    bytesTransformed += blockSize;

                    inputCount -= 64;
                    outputOffset += 64;
                    inputOffset += 64;
                }

                return bytesTransformed;
            }

            // The Salsa20 Core Function reads a 64-byte vector x and produces a 64-byte 
            // vector Salsa20(x). This is the basis of the Salsa20 Stream Cipher. 
            private void Salsa20Core(byte[] output, uint[] input)
            {
                uint[] tmp = (uint[])input.Clone();

                for (int i = numRounds; i > 0; i -= 2)
                {
                    tmp[4] ^= Rotate(Add(tmp[0], tmp[12]), 7);
                    tmp[8] ^= Rotate(Add(tmp[4], tmp[0]), 9);
                    tmp[12] ^= Rotate(Add(tmp[8], tmp[4]), 13);
                    tmp[0] ^= Rotate(Add(tmp[12], tmp[8]), 18);
                    tmp[9] ^= Rotate(Add(tmp[5], tmp[1]), 7);
                    tmp[13] ^= Rotate(Add(tmp[9], tmp[5]), 9);
                    tmp[1] ^= Rotate(Add(tmp[13], tmp[9]), 13);
                    tmp[5] ^= Rotate(Add(tmp[1], tmp[13]), 18);
                    tmp[14] ^= Rotate(Add(tmp[10], tmp[6]), 7);
                    tmp[2] ^= Rotate(Add(tmp[14], tmp[10]), 9);
                    tmp[6] ^= Rotate(Add(tmp[2], tmp[14]), 13);
                    tmp[10] ^= Rotate(Add(tmp[6], tmp[2]), 18);
                    tmp[3] ^= Rotate(Add(tmp[15], tmp[11]), 7);
                    tmp[7] ^= Rotate(Add(tmp[3], tmp[15]), 9);
                    tmp[11] ^= Rotate(Add(tmp[7], tmp[3]), 13);
                    tmp[15] ^= Rotate(Add(tmp[11], tmp[7]), 18);
                    tmp[1] ^= Rotate(Add(tmp[0], tmp[3]), 7);
                    tmp[2] ^= Rotate(Add(tmp[1], tmp[0]), 9);
                    tmp[3] ^= Rotate(Add(tmp[2], tmp[1]), 13);
                    tmp[0] ^= Rotate(Add(tmp[3], tmp[2]), 18);
                    tmp[6] ^= Rotate(Add(tmp[5], tmp[4]), 7);
                    tmp[7] ^= Rotate(Add(tmp[6], tmp[5]), 9);
                    tmp[4] ^= Rotate(Add(tmp[7], tmp[6]), 13);
                    tmp[5] ^= Rotate(Add(tmp[4], tmp[7]), 18);
                    tmp[11] ^= Rotate(Add(tmp[10], tmp[9]), 7);
                    tmp[8] ^= Rotate(Add(tmp[11], tmp[10]), 9);
                    tmp[9] ^= Rotate(Add(tmp[8], tmp[11]), 13);
                    tmp[10] ^= Rotate(Add(tmp[9], tmp[8]), 18);
                    tmp[12] ^= Rotate(Add(tmp[15], tmp[14]), 7);
                    tmp[13] ^= Rotate(Add(tmp[12], tmp[15]), 9);
                    tmp[14] ^= Rotate(Add(tmp[13], tmp[12]), 13);
                    tmp[15] ^= Rotate(Add(tmp[14], tmp[13]), 18);
                }

                for (int i = 0; i < 16; i++)
                {
                    ToBytes(Add(tmp[i], input[i]), output, 4 * i);
                }
            }

            /* Bit Twiddling methods */

            // Serialize the input integer into the output buffer. The input integer 
            // will be split into 4 bytes and put into four sequential places in the 
            // output buffer, starting at the outputOffset. 
            private static void ToBytes(uint input, byte[] output, int outputOffset)
            {
                unchecked
                {
                    output[outputOffset] = (byte)input;
                    output[outputOffset + 1] = (byte)(input >> 8);
                    output[outputOffset + 2] = (byte)(input >> 16);
                    output[outputOffset + 3] = (byte)(input >> 24);
                }
            }

            private static uint Rotate(uint v, int c)
            {
                return (v << c) | (v >> (32 - c));
            }

            // Unchecked integer addition. The Salsa spec defines certain operations 
            // to use 32-bit unsigned integer addition modulo 2^32. 
            private static uint Add(uint v, uint w)
            {
                return unchecked(v + w);
            }

            // Add 1 to the input parameter using unchecked integer addition. The 
            // Salsa spec defines certain operations to use 32-bit unsigned integer 
            // addition modulo 2^32. 
            private static uint AddOne(uint v)
            {
                return unchecked(v + 1);
            }

            // Convert four bytes of the input buffer into an unsigned 
            // 32-bit integer, beginning at the inputOffset.
            private static uint ToUInt32(byte[] input, int inputOffset)
            {
                unchecked
                {
                    return (uint)(((input[inputOffset] |
                                    (input[inputOffset + 1] << 8)) |
                                    (input[inputOffset + 2] << 16)) |
                                    (input[inputOffset + 3] << 24));
                }
            }

            /* ICryptoTransform Overrides */

            // Clear and dispose of the internal Salsa state. 
            public void Dispose()
            {
                if (state != null)
                {
                    Array.Clear(state, 0, state.Length);
                }

                state = null;
            }

            // Determine whether the current transform can be reused (Read-Only) 
            public bool CanReuseTransform
            {
                get
                {
                    return false;
                }
            }

            // Determine whether multiple blocks can be transformed (Read-Only) 
            public bool CanTransformMultipleBlocks
            {
                get
                {
                    return true;
                }
            }

            // Get the input block size, in bytes (Read-Only) 
            public int InputBlockSize
            {
                get
                {
                    return 64;
                }
            }

            // Get the output block size, in bytes (Read-Only) 
            public int OutputBlockSize
            {
                get
                {
                    return 64;
                }












            }
        }
    }
}

