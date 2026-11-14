# Open decisions

The following questions require product, operations, legal, or deployment decisions before the corresponding implementation stage:

1. What customer roles may upload, and is anonymous upload prohibited?
2. What are maximum upload bytes, decompressed bytes, triangles, dimensions, processing time, and per-customer quotas?
3. Which units and mesh tolerances are accepted when a file is unitless or ambiguous?
4. Is 3MF the preferred customer interchange format, and which OBJ/MTL/AMF extensions are required?
5. Which parser libraries may be redistributed, under what license policy, and who owns vulnerability response?
6. Is hosted slicing allowed, on which OS/container platform, and may AGPL engines/profiles be distributed or only invoked in customer infrastructure?
7. Which engine/profile versions are approved for quote calculation, and how are profile changes versioned?
8. What pricing formula, minimum fee, material waste factor, labor rate, machine rate, tax treatment, and rounding rules apply?
9. How long are drafts, rejected assets, quotes, reports, and artifacts retained? What legal hold and GDPR erasure exceptions apply?
10. What native Smartstore representation will an accepted quote use: dedicated product/variant, hidden product, or another supported pricing path?
11. Which quote states, expiry, deposit, cancellation, rejection, revision, and refund policies apply?
12. What production priority, printer allocation, reprint, failure, and customer notification policies apply?
13. What operator roles can adjust stock, override analysis, approve quotes, or mark actual consumption?
14. Which dashboards and profitability reports are required, and what is the source of truth for cost rates?
15. Which stores/locales/regions and data-residency constraints apply?
