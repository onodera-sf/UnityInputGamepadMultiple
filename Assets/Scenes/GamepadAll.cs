using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GamepadAll : MonoBehaviour
{
  /// <summary>����\��������e�L�X�g�I�u�W�F�N�g�ꗗ�B</summary>
  [SerializeField] private Text[] TextObjects;

  StringBuilder Builder = new StringBuilder();
  StringBuilder BuilderButton = new StringBuilder();

  // �X�V�̓t���[�����Ƃ�1��Ăяo����܂�
  void Update()
  {
    if (TextObjects == null)
    {
      Debug.Log($"{nameof(TextObjects)} �� null �ł��B");
      return;
    }

    // Gamepad.all �Őڑ�����Ă��邷�ׂẴQ�[���p�b�h��񋓂ł���
    // TextObjects �̐��ȏ�̏��͍ڂ����Ȃ��̂ŁA���Ȃ����̐��� for ����
    for (int i = 0; i < Gamepad.all.Count || i < TextObjects.Length; i++)
		{
      var gamepad = Gamepad.all[i];
      var textObject = TextObjects[i];

      Builder.Clear();
      BuilderButton.Clear();

      Builder.AppendLine($"deviceId:{gamepad.deviceId}");
      Builder.AppendLine($"name:{gamepad.name}");

      // ���삳�ꂽ�{�^���Ȃǂ̏����擾
      var leftStickValue = gamepad.leftStick.ReadValue();
      var rightStickValue = gamepad.rightStick.ReadValue();
      var dpadValue = gamepad.dpad.ReadValue();

      if (leftStickValue.magnitude > 0f) Builder.AppendLine($"LeftStick:{leftStickValue.normalized * leftStickValue.magnitude}");
      if (rightStickValue.magnitude > 0f) Builder.AppendLine($"RightStick:{rightStickValue.normalized * rightStickValue.magnitude}");
      if (dpadValue.magnitude > 0f) Builder.AppendLine($"Dpad:{dpadValue.normalized * dpadValue.magnitude}");

      if (gamepad.aButton.isPressed) BuilderButton.Append($"A ");
      if (gamepad.bButton.isPressed) BuilderButton.Append($"B ");
      if (gamepad.xButton.isPressed) BuilderButton.Append($"X ");
      if (gamepad.yButton.isPressed) BuilderButton.Append($"Y ");

      if (gamepad.startButton.isPressed) BuilderButton.Append($"Start ");
      if (gamepad.selectButton.isPressed) BuilderButton.Append($"Select ");

      if (gamepad.leftStickButton.isPressed) BuilderButton.Append($"LeftStickButton ");
      if (gamepad.rightStickButton.isPressed) BuilderButton.Append($"RightStickButton ");

      if (gamepad.leftShoulder.isPressed) BuilderButton.Append($"LeftShoulder ");
      if (gamepad.rightShoulder.isPressed) BuilderButton.Append($"RightShoulder ");

      if (BuilderButton.Length >= 1) Builder.AppendLine(BuilderButton.ToString());

      var leftTriggerValue = gamepad.leftTrigger.ReadValue();
      var rightTriggerValue = gamepad.rightTrigger.ReadValue();

      if (leftTriggerValue > 0 || rightTriggerValue > 0)
      {
        Builder.AppendLine($"Trigger:({leftTriggerValue:f2}, {rightTriggerValue:f2})");
      }

      // �擾��������\��
      textObject.text = Builder.ToString();
    }
  }
}
