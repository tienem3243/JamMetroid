using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Ubh util.
/// </summary>
public static class Util
{
    public static readonly Vector3 VECTOR3_ZERO = Vector3.zero;
    public static readonly Vector3 VECTOR3_ONE = Vector3.one;
    public static readonly Vector3 VECTOR3_HALF = new Vector3(0.5f, 0.5f, 0.5f);

    public static readonly Vector2 VECTOR2_ZERO = Vector2.zero;
    public static readonly Vector2 VECTOR2_ONE = Vector2.one;
    public static readonly Vector2 VECTOR2_HALF = new Vector2(0.5f, 0.5f);

    public static readonly Quaternion QUATERNION_IDENTITY = Quaternion.identity;

    /// <summary>
    /// Move axis types.
    /// </summary>
    public enum AXIS
    {
        X_AND_Y,
        X_AND_Z,
    }

    /// <summary>
    /// Time types.
    /// </summary>
    public enum TIME
    {
        DELTA_TIME,
        UNSCALED_DELTA_TIME,
        FIXED_DELTA_TIME,
    }

    /// <summary>
    /// Determines if is mobile platform.
    /// </summary>
    public static bool IsMobilePlatform()
    {
#if UNITY_IOS || UNITY_ANDROID
        return true;
#else
        return false;
#endif
    }

    /// <summary>
    /// Get Transform from tag name.
    /// </summary>
    public static Transform GetTransformFromTagName(string tagName, bool randomSelect, bool nearestSelect, Transform selfTrans,float maxDistance, List<GameObject> ignoreList)
    {
        if (string.IsNullOrEmpty(tagName))
        {
            return null;
        }

        GameObject goTarget = null;
        if (randomSelect || nearestSelect)
        {
            GameObject[] goTargets = GameObject.FindGameObjectsWithTag(tagName);
            if (goTargets != null && goTargets.Length > 0)
            {
                if (randomSelect)
                {
                    goTarget = goTargets[Random.Range(0, goTargets.Length)];
                }
                else if (nearestSelect)
                {
                    Vector3 selfPos = selfTrans.position;
                    float nearrestDistance = float.MaxValue;
                    int nearrestIndex = -1;
                    
                    for (int i = goTargets.Length - 1; i >= 0; i--)
                    {
                        GameObject go = goTargets[i];
                        if (ignoreList != null && ignoreList.Contains(go)) continue;
                        float distance = Vector3.Distance(go.transform.position, selfPos);
                        
                        if (nearrestIndex < 0 || distance <= nearrestDistance)
                        {
                            nearrestIndex = i;
                            nearrestDistance = distance;
                        }
                    }

                    if (nearrestIndex >= 0)
                    {
                        goTarget = goTargets[nearrestIndex];
                    }
                
                    if (nearrestDistance >= maxDistance)
                    {
                        goTarget = null;
                    }
                }
            }
        }
        else
        {
            goTarget = GameObject.FindWithTag(tagName);
        }

        if (goTarget == null)
        {
        
            return null;
        }

        return goTarget.transform;
    }

   
        /// <summary>
        /// Get shifted angle.
        /// </summary>
        public static float GetShiftedAngle(int wayIndex, float baseAngle, float betweenAngle)
    {
        float angle = wayIndex % 2 == 0 ?
                      baseAngle - (betweenAngle * (float)wayIndex / 2f) :
                      baseAngle + (betweenAngle * Mathf.Ceil((float)wayIndex / 2f));
        return angle;
    }

    /// <summary>
    /// Get 0 ~ 360 angle.
    /// </summary>
    /// 
    public static Vector3 Intersect(Vector3 line1V1, Vector3 line1V2, Vector3 line2V1, Vector3 line2V2)
    {
        //Line1
        float A1 = line1V2.y - line1V1.y;
        float B1 = line1V1.x - line1V2.x;
        float C1 = A1 * line1V1.x + B1 * line1V1.y;

        //Line2
        float A2 = line2V2.y - line2V1.y;
        float B2 = line2V1.x - line2V2.x;
        float C2 = A2 * line2V1.x + B2 * line2V1.y;

        //Crammer
        float delta = A1 * B2 - A2 * B1;

        if (delta == 0)
            throw new ArgumentException("Lines are parallel");

        float x = (B2 * C1 - B1 * C2) / delta;
        float y = (A1 * C2 - A2 * C1) / delta;
        return new Vector3(x, y);
    }
    public static float GetNormalizedAngle(float angle)
    {
        while (angle < 0f)
        {
            angle += 360f;
        }
        while (360f < angle)
        {
            angle -= 360f;
        }
        return angle;
    }

    /// <summary>
    /// Get angle from two transforms position.
    /// </summary>
    public static float GetAngleFromTwoPosition(Transform fromTrans, Transform toTrans, AXIS axisMove)
    {
        switch (axisMove)
        {
            case AXIS.X_AND_Y:
                return GetZangleFromTwoPosition(fromTrans, toTrans);
            case AXIS.X_AND_Z:
                return GetYangleFromTwoPosition(fromTrans, toTrans);
            default:
                return 0f;
        }
    }

    /// <summary>
    /// Get Z angle from two transforms position.
    /// </summary>
    private static float GetZangleFromTwoPosition(Transform fromTrans, Transform toTrans)
    {
        if (fromTrans == null || toTrans == null)
        {
            return 0f;
        }
        float xDistance = toTrans.position.x - fromTrans.position.x;
        float yDistance = toTrans.position.y - fromTrans.position.y;
        float angle = (Mathf.Atan2(yDistance, xDistance) * Mathf.Rad2Deg) - 90f;
        angle = GetNormalizedAngle(angle);

        return angle;
    }

    /// <summary>
    /// Get Y angle from two transforms position.
    /// </summary>
    private static float GetYangleFromTwoPosition(Transform fromTrans, Transform toTrans)
    {
        if (fromTrans == null || toTrans == null)
        {
            return 0f;
        }
        float xDistance = toTrans.position.x - fromTrans.position.x;
        float zDistance = toTrans.position.z - fromTrans.position.z;
        float angle = (Mathf.Atan2(zDistance, xDistance) * Mathf.Rad2Deg) - 90f;
        angle = GetNormalizedAngle(angle);
        return angle;
    }
    public static Vector3 CalculateCentroid(Vector3[] centerPoints)
    {
        Vector3 vector = new Vector3(0, 0, 0);
        var numPoints = centerPoints.Length;
        foreach (var point in centerPoints)
        {
            vector += point;
        }

        vector /= numPoints;

        return vector;
    }
    public static Vector3 CalculatePositionOnPath(Vector3[] path,float offset,bool isCircle,out int currentWaypoint)
    {
        currentWaypoint = 0;
        if (path.Length < 2)
            return path[0];

        Vector3 circlePosition = Vector3.zero;
        float totalDistance = 0f;
        for (int i = 0; i < path.Length - 1; i++)
        {
            totalDistance += Vector3.Distance(path[i], path[i + 1]);
        }
        if(isCircle) totalDistance += Vector3.Distance(path[0], path[path.Length - 1]);
        float normalizedOffset = (offset % totalDistance + totalDistance) % totalDistance;
        float remainingOffset = Mathf.Abs(normalizedOffset);

        for (int i = 0; i < path.Length; i++)
        {
            int currentIndex = i;
            int nextIndex = (i + 1) % path.Length;

            Vector3 currentPoint = path[currentIndex];
            Vector3 nextPoint = path[nextIndex];
            float distance = Vector3.Distance(currentPoint, nextPoint);

            if (remainingOffset <= distance)
            {
                float t = Mathf.Clamp01(remainingOffset / distance);
                circlePosition = Vector3.Lerp(currentPoint, nextPoint, t);
                break;
            }
            else
            {
                remainingOffset -= distance;
            }
        }

        if (offset < 0)
        {
            Vector3 startPoint = path[path.Length - 1];
            Vector3 endPoint = path[0];
            Vector3 startToEnd = endPoint - startPoint;
            circlePosition = startPoint - (circlePosition - endPoint);
        }
        return circlePosition;
    }
    public static Vector3 CalculatePositionOnPath(Vector3[] waypoints, float offset,bool isCircle)
    {
        return CalculatePositionOnPath(waypoints, offset,isCircle, out int path);
    }

    public static int CalculateWaypointOnPath(Vector3[] waypoints, float position,bool isCircle)
    {
        CalculatePositionOnPath(waypoints, position,isCircle, out int a);
        return a;
    }
    public static void LookAtLerp(Transform _transform,Transform target,float speed=1)
    {
        var lookPos = target.position - _transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, rotation,speed* Time.deltaTime);
    }
}