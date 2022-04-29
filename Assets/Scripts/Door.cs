using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lesson1
{
    public class Door : MonoBehaviour
    {
        public enum open_type_ENUM { rot_to_open, move_to_open } //тип открытия двери
        public open_type_ENUM open_Type;
        public enum door_axis_ENUM { X, Y, Z } //ось вращения или движения
        public door_axis_ENUM door_axis;
        public bool only_open; // если можно только открыть
        public bool can_be_opened_now; //можно ли открыть сейчас
        private bool is_open; //true если уже открыта
        public float open_speed = 150f; //скорость открытия двери(вращение 150. сдвиг 2)
        public float open_dist_or_angle = 140f; //угол или расстояние открывания (вращение 90-140, сдвиг 1)

        public AudioSource move_or_rot_sound;
        public AudioSource open_sound;
        public AudioSource close_sound;
        public AudioSource not_opened_sound;

        public GameObject door_handle;//ручка двери

        public enum handle_axis_ENUM { X, Y, Z } //ось вращения ручки
        public handle_axis_ENUM handle_axis;
        public float handle_rot_angle = -45f;
        private Quaternion handle_start_rot;
        private float start_dist_or_angle; //начальный угол или позиция
        private bool open_close_ON; //откр. и закр. в данный момент(чтоб в апдейте не работало постоянно)

        public GameObject interaction_Image;


        private void Start()
        {
            if(open_Type == open_type_ENUM.move_to_open)
            {
                if (door_axis == door_axis_ENUM.X) start_dist_or_angle = transform.localPosition.x;
                else if (door_axis == door_axis_ENUM.Y) start_dist_or_angle = transform.localPosition.y;
                else if (door_axis == door_axis_ENUM.Z) start_dist_or_angle = transform.localPosition.z;
            }
            else 
            {
                if (door_axis == door_axis_ENUM.X) start_dist_or_angle = transform.localEulerAngles.x;
                else if (door_axis == door_axis_ENUM.Y) start_dist_or_angle = transform.localEulerAngles.y;
                else if (door_axis == door_axis_ENUM.Z) start_dist_or_angle = transform.localEulerAngles.z;
            }
            if (door_handle) handle_start_rot = door_handle.transform.localRotation;
        }

        
        private void OnMouseEnter()
        {
            if (gameObject.tag == "Door") interaction_Image.SetActive(true);
        }

        private void OnMouseExit()
        {
            if (gameObject.tag == "Door") interaction_Image.SetActive(false);
        }

        private void OnMouseDown()
        {
            if (gameObject.tag == "Door")
            { 
                if (door_handle)
                {
                    if (handle_axis == handle_axis_ENUM.X) door_handle.transform.localRotation = Quaternion.Euler(handle_rot_angle, 0f, 0f);
                    else if (handle_axis == handle_axis_ENUM.Y) door_handle.transform.localRotation = Quaternion.Euler(0f, handle_rot_angle, 0f);
                    else if (handle_axis == handle_axis_ENUM.Z) door_handle.transform.localRotation = Quaternion.Euler(0f, 0f, handle_rot_angle);
                }
                Open_close();
            }
        }

        private void OnMouseUp()
        {
            if (door_handle) door_handle.transform.localRotation = handle_start_rot;
        }

        private void Update()
        {
            if (open_close_ON)
            {
                if (is_open) //открываем дверь
                {
                    if (open_Type == open_type_ENUM.move_to_open)
                    {
                        if (door_axis == door_axis_ENUM.X)
                        {
                            float posX = Mathf.MoveTowards(transform.localPosition.x, start_dist_or_angle + open_dist_or_angle, open_speed * Time.deltaTime);
                            transform.localPosition = new Vector3(posX, transform.localPosition.y, transform.localPosition.z);
                            if (transform.localPosition.x == start_dist_or_angle + open_dist_or_angle) Stop_open_close();
                        }
                        else if (door_axis == door_axis_ENUM.Y)
                        {
                            float posY = Mathf.MoveTowards(transform.localPosition.y, start_dist_or_angle + open_dist_or_angle, open_speed * Time.deltaTime);
                            transform.localPosition = new Vector3(transform.localPosition.x, posY, transform.localPosition.z);
                            if (transform.localPosition.y == start_dist_or_angle + open_dist_or_angle) Stop_open_close();
                        }
                        else if (door_axis == door_axis_ENUM.Z)
                        {
                            float posZ = Mathf.MoveTowards(transform.localPosition.z, start_dist_or_angle + open_dist_or_angle, open_speed * Time.deltaTime);
                            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, posZ);
                            if (transform.localPosition.z == start_dist_or_angle + open_dist_or_angle) Stop_open_close();
                        }
                    }
                    else //вращение
                    {
                        if (door_axis == door_axis_ENUM.X)
                        {
                            float angleX = Mathf.MoveTowardsAngle(transform.localEulerAngles.x, start_dist_or_angle + open_dist_or_angle, open_speed * Time.deltaTime);
                            transform.localEulerAngles = new Vector3(angleX, 0, 0);
                            if (transform.localEulerAngles.x == start_dist_or_angle + open_dist_or_angle) Stop_open_close();
                        }
                        else if (door_axis == door_axis_ENUM.Y)
                        {
                            float angleY = Mathf.MoveTowardsAngle(transform.localEulerAngles.y, start_dist_or_angle + open_dist_or_angle, open_speed * Time.deltaTime);
                            transform.localEulerAngles = new Vector3(0, angleY, 0);
                            if (transform.localEulerAngles.y == start_dist_or_angle + open_dist_or_angle) Stop_open_close();
                        }
                        else if (door_axis == door_axis_ENUM.Z)
                        {
                            float angleZ = Mathf.MoveTowardsAngle(transform.localEulerAngles.z, start_dist_or_angle + open_dist_or_angle, open_speed * Time.deltaTime);
                            transform.localEulerAngles = new Vector3(0, 0, angleZ);
                            if (transform.localEulerAngles.z == start_dist_or_angle + open_dist_or_angle) Stop_open_close();
                        }
                    }
                }
                else //Закрываем дверь
                {
                    if (open_Type == open_type_ENUM.move_to_open) //движение
                    {
                        if (door_axis == door_axis_ENUM.X)
                        {
                            float posX = Mathf.MoveTowards(transform.localPosition.x, start_dist_or_angle, open_speed * Time.deltaTime);
                            transform.localPosition = new Vector3(posX, transform.localPosition.y, transform.localPosition.z);
                            if (transform.localPosition.x == start_dist_or_angle) Stop_open_close();
                        }
                        else if (door_axis == door_axis_ENUM.Y)
                        {
                            float posY = Mathf.MoveTowards(transform.localPosition.y, start_dist_or_angle, open_speed * Time.deltaTime);
                            transform.localPosition = new Vector3(transform.localPosition.x, posY, transform.localPosition.z);
                            if (transform.localPosition.y == start_dist_or_angle) Stop_open_close();
                        }
                        else if (door_axis == door_axis_ENUM.Z)
                        {
                            float posZ = Mathf.MoveTowards(transform.localPosition.z, start_dist_or_angle, open_speed * Time.deltaTime);
                            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, posZ);
                            if (transform.localPosition.z == start_dist_or_angle) Stop_open_close();
                        }
                    }
                    else //вращение
                    {
                        if (door_axis == door_axis_ENUM.X)
                        {
                            float angleX = Mathf.MoveTowardsAngle(transform.localEulerAngles.x, start_dist_or_angle, open_speed * Time.deltaTime);
                            transform.localEulerAngles = new Vector3(angleX, 0, 0);
                            if (transform.localEulerAngles.x == start_dist_or_angle) Stop_open_close();
                        }
                        else if (door_axis == door_axis_ENUM.Y)
                        {
                            float angleY = Mathf.MoveTowardsAngle(transform.localEulerAngles.y, start_dist_or_angle, open_speed * Time.deltaTime);
                            transform.localEulerAngles = new Vector3(0, angleY, 0);
                            if (transform.localEulerAngles.y == start_dist_or_angle) Stop_open_close();
                        }
                        else if (door_axis == door_axis_ENUM.Z)
                        {
                            float angleZ = Mathf.MoveTowardsAngle(transform.localEulerAngles.z, start_dist_or_angle, open_speed * Time.deltaTime);
                            transform.localEulerAngles = new Vector3(0, 0, angleZ);
                            if (transform.localEulerAngles.z == start_dist_or_angle) Stop_open_close();
                        }
                    }
                }
            }
        }




            public void Open_close()
            {
                if(can_be_opened_now)
                {
                    if (move_or_rot_sound) move_or_rot_sound.Play();
                    open_close_ON = true;
                    if (is_open) is_open = false;
                    else 
                    {
                        is_open = true;
                        if (open_sound) open_sound.Play();
                        if(only_open)
                        {
                            gameObject.tag = "Untegged"; // чтобы дверь больше не была интерактивна
                            interaction_Image.SetActive(false);
                        }

                    }
                }
                else
                {
                    if (not_opened_sound) not_opened_sound.Play();
                    print("CLOSE!!!");           
                }


            }
        void Stop_open_close()
        {
            open_close_ON = false;
            if (move_or_rot_sound) move_or_rot_sound.Stop();
            if (close_sound && !is_open) close_sound.Play();
        }


    }
}