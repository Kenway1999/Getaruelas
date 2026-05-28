using System;
using System.Text.Json;

namespace StudentConsultationLogs.Models
{
    public class Record
    {
        public string RecordId { get; set; }
        public string StudentName { get; set; }
        public string StudentId { get; set; }
        public string AdvisorName { get; set; }
        public string ConsultationNotes { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string Checksum { get; set; }

        public static Record Create(string studentName, string studentId, string advisorName, string notes)
        {
            var now = DateTime.UtcNow.ToString("o");
            return new Record
            {
                RecordId = Guid.NewGuid().ToString("D"),
                StudentName = studentName.Trim(),
                StudentId = studentId.Trim(),
                AdvisorName = advisorName.Trim(),
                ConsultationNotes = notes?.Trim() ?? "",
                CreatedAt = now,
                UpdatedAt = now,
                IsActive = true,
                Checksum = ""
            }.WithComputedChecksum();
        }

        public void Update(string studentName, string studentId, string advisorName, string notes)
        {
            StudentName = studentName.Trim();
            StudentId = studentId.Trim();
            AdvisorName = advisorName.Trim();
            ConsultationNotes = notes?.Trim() ?? "";
            UpdatedAt = DateTime.UtcNow.ToString("o");
            Checksum = ComputeChecksum();
        }

        public void SoftDelete()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow.ToString("o");
            Checksum = ComputeChecksum();
        }

        public string ComputeChecksum()
        {
            return ChecksumUtil.ComputeSha256(
                $"{RecordId}{StudentName}{StudentId}{AdvisorName}{ConsultationNotes}{CreatedAt}{UpdatedAt}{IsActive}"
            );
        }

        public Record WithComputedChecksum()
        {
            Checksum = ComputeChecksum();
            return this;
        }

        public bool VerifyChecksum()
        {
            return string.Equals(Checksum, ComputeChecksum(), StringComparison.OrdinalIgnoreCase);
        }

        public string ToJsonLine() => JsonSerializer.Serialize(this);

        public static Record FromJsonLine(string jsonLine) => JsonSerializer.Deserialize<Record>(jsonLine)!;
    }
}
