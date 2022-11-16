using System;
using System.Collections.Generic;

public class SchoolManager
{
    public static void UniteSchools(List<School> schools, List<string> schoolNumbers)
    {
        var schoolsUniteIndexes = new List<int>();
        for (int i = 0; i < schools.Count; i++)
            if (schoolNumbers.Contains(schools[i].SchoolNumber))
                schoolsUniteIndexes.Add(i);

        string mainNumber = schools[schoolsUniteIndexes[0]].SchoolNumber;
        for (int i = 0; i < schoolsUniteIndexes.Count; i++)
        {
            var school = schools[schoolsUniteIndexes[i]];
            school.SchoolNumber = $"{mainNumber}-{i+1} was united";
            foreach (var pupil in school.Pupils)
                pupil.SchoolNumber = $"{mainNumber}-{i + 1}";
        }
    }

    public static void TransferPupil(List<Pupil> pupils, List<School> schools, int pupilId, string newSchoolNumber)
    {
        int pupilIndex = -1;
        for (int i = 0; i < pupils.Count; i++)
            if (pupils[i].Id == pupilId)
                pupilIndex = i;

        int curSchoolIndex = -1, newSchoolIndex = -1;
        for (int i = 0; i < schools.Count; i++)
        {
            if (schools[i].SchoolNumber == pupils[pupilIndex].SchoolNumber)
                curSchoolIndex = i;
            if (schools[i].SchoolNumber == newSchoolNumber)
                newSchoolIndex = i;
        }

        var pupil = pupils[pupilIndex];
        schools[curSchoolIndex].Pupils.Remove(pupil);
        schools[newSchoolIndex].Pupils.Add(pupil);
        pupil.SchoolNumber = schools[newSchoolIndex].SchoolNumber;
    }

    public static void CloseSchool(List<School> schools, string schoolNumber, string newSchoolNumber)
    {
        int curSchoolIndex = -1, newSchoolIndex = -1;
        for (int i = 0; i < schools.Count; i++)
        {
            if (schools[i].SchoolNumber == schoolNumber)
                curSchoolIndex = i;
            if (schools[i].SchoolNumber == newSchoolNumber)
                newSchoolIndex = i;
        }

        foreach (var pupil in schools[curSchoolIndex].Pupils)
        {
            schools[curSchoolIndex].Pupils.Remove(pupil);
            schools[newSchoolIndex].Pupils.Add(pupil);
            pupil.SchoolNumber = schools[newSchoolIndex].SchoolNumber;
        }
        schools[curSchoolIndex].SchoolNumber += " was closed";
    }



    
}