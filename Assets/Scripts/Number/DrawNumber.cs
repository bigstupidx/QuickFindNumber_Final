using UnityEngine;

public class DrawNumber : MonoBehaviour
{
    /// <summary>
    /// Tính toán vị trí ngẫu nhiên cho các con số
    /// </summary>
    /// <param name="panelWidth"> Chiều ngang của panel chứa các con số</param>
    /// <param name="panelHeight"> Chiều cao của panel chứa các con số</param>
    /// <param name="numberCount"> Số lượng con số</param>
    /// <param name="positions"> Mảng chứa vị trí của từng con số</param>
    /// <param name="marginLeft"> Căn lề trái</param>
    /// <param name="marginRight"> Căn lề phải</param>
    /// <param name="marginTop"> Căn lề trên</param>
    /// <param name="marginBottom"> Căn lề dưới</param>
    /// <param name="type"> Loại kiểu chơi: 1 người hoặc 2 người</param>
    public void RandomPossition(float panelWidth, float panelHeight, int numberCount, Vector2[] positions, float marginLeft, float marginRight, float marginTop, float marginBottom, int type, int level)
    {
        Vector2[] _positionsNumber = new Vector2[numberCount];

        // Cờ báo khi vị trí 2 số gần nhau quá, (flag = false) thì tính lại
        bool flag = true;

        for (int i = 0; i < numberCount; i++)
        {
            if (i == 0)
            {
                _positionsNumber[i].x = Random.Range(-panelWidth / 2 + marginLeft, panelWidth / 2 - marginRight);
                _positionsNumber[i].y = Random.Range(-panelHeight / 2 + marginBottom, panelHeight / 2 - marginTop);
            }

            if (i > 0)
            {
                // Tính toán sao cho khoảng cách giữa các con số đều nhau
                do
                {
                    flag = true;
                    _positionsNumber[i].x = Random.Range(-panelWidth / 2 + marginLeft, panelWidth / 2 - marginRight);
                    _positionsNumber[i].y = Random.Range(-panelHeight / 2 + marginBottom, panelHeight / 2 - marginTop);

                    for (int j = 0; j < i; j++)
                    {
                        if (CompareVector(_positionsNumber[i], _positionsNumber[j], type, level) == false)
                        {
                            flag = false;
                            break;
                        }
                    }
                } while (flag == false);

            }

            positions[i] = _positionsNumber[i];
        }

    }

    // Hàm so sánh vị trí của 2 điểm
    public bool CompareVector(Vector2 a, Vector2 b, int type, int level)
    {
        // Type: 1 player
        if (type == 1)
        {
            if (level == 0)
            {
                if (Mathf.Abs(a.x - b.x) < 70f && Mathf.Abs(a.y - b.y) < 68f)
                    return false;
                else
                    return true;
            }
            else
            {
                if (Mathf.Abs(a.x - b.x) < 68f && Mathf.Abs(a.y - b.y) < 65f)
                    return false;
                else
                    return true;
            }

        }
        else  // Type: 2 player
        {
            if (Mathf.Abs(a.x - b.x) < 60f && Mathf.Abs(a.y - b.y) < 50f)
                return false;
            else
                return true;
        }

    }
    
}
